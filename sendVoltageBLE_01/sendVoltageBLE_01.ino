#include <ArduinoBLE.h>

BLEService micService("19B10000-E8F2-537E-4F6C-D104768A1214");  // replace with your service UUID

BLEFloatCharacteristic voltageCharacteristic("19B10001-E8F2-537E-4F6C-D104768A1214", BLERead | BLENotify);  // replace with your characteristic UUID

const int micPin = A0;

void setup() {
  Serial.begin(9600);

  if (!BLE.begin()) {
    Serial.println("Starting BLE failed!");
    while (1)
      ;
  }

  BLE.setLocalName("MicPeripheral");
  BLE.setAdvertisedService(micService);

  micService.addCharacteristic(voltageCharacteristic);
  BLE.addService(micService);

  BLE.advertise();
  Serial.println("BLE peripheral waiting to be connected...");
}

void loop() {
  BLEDevice central = BLE.central();

  if (central) {
    Serial.print("Connected to central: ");
    Serial.println(central.address());

    while (central.connected()) {
      // Read the analog value from the microphone
      int micValue = analogRead(micPin);

      // Convert the analog value to voltage (assuming a 5V reference)
      float voltage = micValue * (5.0 / 1023.0);

      // Update the BLE characteristic with the voltage value
      voltageCharacteristic.writeValue(voltage);

      // Print the voltage to Serial Monitor
      Serial.print("Voltage: ");
      Serial.println(voltage);

      delay(1000);  // Adjust the delay based on your application requirements
    }

    Serial.print("Disconnected from central: ");
    Serial.println(central.address());
  }
}