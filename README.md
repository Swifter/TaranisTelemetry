The Taranis Telemetry Listener is a C# class library and example Winforms application designed to receive and decode the telemetry feed from the RS-232 port on the Taranis RC transmitter.  The serial code is based on a Microsoft sample.  Any file with the GPL license comments at the top is actually part of the TaranisTelemetry solution, everything else is based on the original Microsoft serial port sample.  Later, I'll make this a separate assembly, but this code isn't ready for that quite yet.

The code consists of the following source files:

Program.cs - standard Winforms Main()

App.config - a configuration file for the Winforms application, for with config information for the log4net .Net logging system.

Mainform.cs - The main Winforms window. See UserInitialization() to see how to bind Winforms textbox controls to properties on the FrskyTelemetryViewModel.

packages.config - The Nuget package manager file used by Visual Studio to download the dependent log4net assembly.  TaranisTelemetry.cs depend on this.

FrskyTelemetryViewModel.cs - The Winforms/WPF/WinRT view model for the Taranis Telemetry data. Each property represents a different telemetry value from a sensor.  This class implements the IFrskyTelemetryData interface, and INotifyPropertyChanged for the .Net property system.

IFrskyTelemetryData.cs - The C# Interface class, implemented by FrskyTelemetryViewModel.cs.  There is a IFrskyTelemetryData public member in the TaranisTelemetry class, and this is where the value of the actual FrskyTelemetryViewModel class instance is placed so the view model can be updated for each actual telemetry value decoded.  If you build your own view model class, you need to implement this interface.

SerialPortManager.cs - The .Net code that actually sets up the serial interface and listens for data.  

TaranisTelemetry.cs - Contains the TaranisTelemetry static class, support classes and utility functions used during decoding.  Will automatically update an attached IFrskyTelemetryData-derived view model class, an example of which is the FrskyTelemetryDataViewModel.

I currently process Frsky SPORT packets only, though the hooks are in place to add decoding for the Hub sensors.  I only have two sensors to test with, the Variometer and LIPO voltage sensor.  The code works well with those sensors.  I'll do the hub function next, but I wanted to get this out there first for comments on the main SPORT decoding function.

What I need done is for the Open-Tx software guys to review the TaranisTelemetry.cs implementation of this function:

private static void frskySportProcessPacket(byte[] packet)

to ensure that I've implemented decoding correctly.  I also need for people with different sensors to test their sensors with this code. 

Specifically, there are decoding rules in this function that I cannot implement on the PC side because I don't have access to the same data and code that this function uses on the actual Taranis device.  All I have to go on is the serial feed.

This application will write a log file in the same directory as the .exe file.  The file is not circular, so it'll get big if you don't delete it from time to time.  I'll be making this optional soon.

Also, it is not apparent to me what all the decoded telemetry values actually mean, and some comments here by the cognescenti would be appreciated.  The code does seem to work well so far.

The code is currently inside a Visual Studio Professional 2013 solution, built for .Net 4.0.  I think it will work fine with the free Visual Studio 2013 Express version (free), but I haven't tested this.

Thanks, and good luck!

Paula Scholz, swiftress@outlook.com
September 1, 2014