# .NET-MAUI-BLE
## Prerequisite
- Follow the instructions from the following website. (https://learn.microsoft.com/en-us/dotnet/maui/get-started/installation?view=net-maui-8.0&tabs=vswin#install-visual-studio-code-and-the-net-maui-extension)
- Test run the sample project (https://learn.microsoft.com/en-us/dotnet/maui/get-started/first-app?view=net-maui-8.0&tabs=visual-studio-code&pivots=devices-android)

## How to debug
### How to find correct characteristicUUID
- On MainPage.xaml.cs file, comment out from line 59 to line 63.
- Add a breakpoint to line 62, `var a = 1` .
- Modify line 13, `string serviceUUID = "180d"` to the device ServiceUUID.
- Run the app with debugging option on VSCode.
- Wait till breakpoint gets hit.
  - If breakpoint does not get hit, check whether if ServiceUUID is correct
- Once the breakpoint gets hit, check all available characteristicUUID on its ServiceUUID.
- Modify line 14, `string characteristicUUID = "2a37";` with the found characteristicUUID.
- Comment out the from line 59 to line 63.
### How to check the data format
- Add breakpoint to line 69 `if (data != null && data.Length > 0) {`
- Run the app with debugging option on VSCode
- Once the breakpoint gets hit, check the data format, `var data = notification.Data;`.
