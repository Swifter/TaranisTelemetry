/* Taranis Telemetry Listener RS-232 serial feed decoder.
 * Authors (alphabetical order)
 * - Paul Scholz, swifterone@hotmail.com
 *
 *
 * opentx is based on code named
 * gruvin9x by Bryan J. Rentoul: http://code.google.com/p/gruvin9x/,
 * er9x by Erez Raviv: http://code.google.com/p/er9x/,
 * and the original (and ongoing) project by
 * Thomas Husterer, th9x: http://code.google.com/p/th9x/
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License version 2 as
 * published by the Free Software Foundation.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 */

// note:  Normally, my .Net code is chock-a-block-full of TryCatch blocks for exception handling,
// but there is none here because I want every exception to stop execution until I am confident 
// the decoding is correct.  TryCatch is certainly on the to-do list.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;

// for [StructLayout] directive, the C# equivalant of PACK
using System.Runtime.InteropServices;

namespace TaranisTelemetryListener
{

    // This is implemented as a class, not a struct as in the Taranis source
    // because in c#, anything bigger than a couple elements should be a class
    // derived from System.Object
    //
    // Note the similarity of variable names and data types to the original code.
    // C# or .Net specific types without analogs in the original code are limited to 
    // the string type.
    public class FrskySerialData
    {
        public Int16 spare1;
        public Int16 gpsAltitude_bp;    // 0x01  before punct
        public Int16 temperature1;      // 0x02   -20 .. 250 deg. celcius
        public UInt16 rpm;              // 0x03   0..60,000 revs. per minute
        public UInt16 fuelLevel;        // 0x04   0, 25, 50, 75, 100 percent
        public Int16 temperature2;      // 0x05   -20 .. 250 deg. celcius
        public UInt16 volts;            // 0x06   1/500V increments (0..4.2V)
        public UInt32 distFromEarthAxis;//        2 spares reused
        public Int16 gpsAltitude_ap;    // 0x01+8 after punct
        public UInt16[] spare2 = new UInt16[6];
        public Int16 baroAltitude_bp;  // 0x10   0..9,999 meters
        public  UInt16 gpsSpeed_bp;      // 0x11   before punct
        public  UInt16 gpsLongitude_bp;  // 0x12   before punct
        public  UInt16 gpsLatitude_bp;   // 0x13   before punct
        public  UInt16 gpsCourse_bp;     // 0x14   before punct (0..359.99 deg. -- seemingly 2-decimal precision)
        public  byte day;              // 0x15
        public  byte month;            // 0x15
        public  UInt16 year;             // 0x16
        public  byte hour;             // 0x17
        public  byte min;              // 0x17
        public  UInt16 sec;              // 0x18
        public  UInt16 gpsSpeed_ap;      // 0x11+8
        public  UInt16 gpsLongitude_ap;  // 0x12+8
        public  UInt16 gpsLatitude_ap;   // 0x13+8
        public  UInt16 gpsCourse_ap;     // 0x14+8
        public  UInt32 pilotLatitude;    //        2 spares reused
        public  UInt32 pilotLongitude;   //        2 spares reused
        public  UInt16 baroAltitude_ap;  // 0x21   after punct
        public  string gpsLongitudeEW;   // 0x1A+8 East/West
        public  string gpsLatitudeNS;    // 0x1B+8 North/South
        public  Int16 accelX;           // 0x24   1/256th gram (-8g ~ +8g)
        public  Int16 accelY;           // 0x25   1/256th gram (-8g ~ +8g)
        public  Int16 accelZ;           // 0x26   1/256th gram (-8g ~ +8g)
        public  Int16 spare3;
        public  UInt16 current;          // 0x28   Current
        public  UInt16[] spare4 = new UInt16[7];
        public  Int16 varioSpeed;       // 0x30  Vertical speed in cm/s

        public  Int32 baroAltitudeOffset;
        public  Int32 baroAltitude;
        public  Int32 gpsAltitudeOffset;
        public  UInt32 gpsDistance;

        public  UInt16 vfas;             // 0x39  Added to FrSky protocol for home made sensors with a better precision
        public  UInt16 volts_bp;         // 0x3A
        public  UInt16 volts_ap;         // 0x3B
        public  UInt16[] spare5 = new UInt16[4];
        // end of FrSky Hub data

        /* these fields must keep this order! */
        public Int16 minAltitude;
        public  Int16 maxAltitude;
        public  UInt16 maxRpm;
        public  Int16 maxTemperature1;
        public  Int16 maxTemperature2;
        public  UInt16 maxGpsSpeed;
        public  UInt16 maxGpsDistance;
        public  UInt16 maxAirSpeed;
        public  Int16 minCell;
        public  Int16 minCells;
        public  Int16 minVfas;
        public  UInt16 maxCurrent;
        public  UInt16 maxPower;
        /* end */

        public  byte gpsDistNeeded = 1;  //        1bits out of 16bits spare reused
        public  byte gpsFix = 2;         //        2bits out of 16bits spare reused: -1=never fixed, 0=not fixed now, 1=fixed
        public  byte openXsensor = 1;    //        1bits out of 16bits spare reused: we receive data from the openXsensor
        public  byte varioHighPrecision = 1;
        public  byte spare6 = 3;

        public  Int32 gpsAltitude;

        public  UInt16 currentConsumption;
        public  UInt16 currentPrescale;
        public  UInt16 power;

        public  byte cellsCount;
        public  byte[] sensorCellsCount = new byte[2];
        public  UInt16[] cellVolts = new UInt16[12];
        public  Int16 cellsSum;
        public  UInt16 cellsState;
        public  UInt16 minCellVolts;

        public  UInt16 airSpeed;
        public  UInt16 dTE;
    }

    public class FrskyValueWithMin
    {
        public byte value;  // filtered value (average of last TaranisTelemetry.TelemetryAverageCount+1 values
        public byte min;
        public byte[] values = new byte[TaranisTelemetry.TelemetryAverageCount];

        public virtual void set(byte newValue)
        {
            if (0 == value)
            {
                // initialize on first set
                values[0] = newValue;
                values[1] = newValue;
                values[2] = newValue;
                value = newValue;
            }
            else
            {
                UInt32 sum = values[0];

                // calculate the average from values[] and value
                // also shift readings in values[] array
                for (int i=0; i < TaranisTelemetry.TelemetryAverageCount - 1; i++)
                {
                    byte tmp = values[i + 1];
                    values[i] = tmp;
                    sum += tmp;
                }

                values[TaranisTelemetry.TelemetryAverageCount - 1] = newValue;

                sum += newValue;

                // no loss of precision with explicit cast as it is an average
                value = (byte)(sum / (TaranisTelemetry.TelemetryAverageCount + 1));
            }

            // test against the min and set it if need be
            if(0 == min || newValue < min)
            {
                min = newValue;
            }
        }
    }

    public class FrskyValueWithMinMax : FrskyValueWithMin
    {
        public byte max;

        // got rid of the test for UNIT_VOLTS as redundant
        public override void set(byte newValue)
        {
            base.set(newValue);

            if(0 == max || newValue > max)
            {
                max = value;
            }
        }
    }

    public static class FrskyData
    {
        public static FrskyValueWithMinMax[] analog = new FrskyValueWithMinMax[5];

        public static FrskyValueWithMin[] rssi = new FrskyValueWithMin[2];
        public static FrskyValueWithMin swr = new FrskyValueWithMin();

        public static FrskySerialData hub = new FrskySerialData();

        // static constructor to init static data
        static FrskyData()
        {
            for (int i = 0; i < analog.Length; i++)
            {
                analog[i] = new FrskyValueWithMinMax();
            }

            for (int i = 0; i < rssi.Length; i++)
            {
                rssi[i] = new FrskyValueWithMin();
            }
        }
    }

    // a convenient wrapper.
    public class FrskyGeoLocation
    {
        public UInt32 Lat;
        public UInt32 Lon;

        public FrskyGeoLocation(UInt32 latitude, UInt32 longitude)
        {
            Lat = latitude;
            Lon = longitude;
        }

        // parameterless constructor
        public FrskyGeoLocation() { }
    }

    // this is the main decoding class, all static.
    public static class TaranisTelemetry
    {
        // right now, we take a dependency on this log4net logger, until the code is a 
        // little more mature.
        private static readonly ILog log = LogManager.GetLogger(typeof(TaranisTelemetry));

        public enum FrSkyDataState
        {
            STATE_DATA_IDLE,
            STATE_DATA_START,
            STATE_DATA_IN_FRAME,
            STATE_DATA_XOR,
            STATE_DATA_PRIVATE_LEN,
            STATE_DATA_PRIVATE_VALUE
        }

        public enum TelemetryStates
        {
            TELEMETRY_INIT,
            TELEMETRY_OK,
            TELEMETRY_KO
        }

        public enum TelemAnas 
        {
          TELEM_ANA_A1,
          TELEM_ANA_A2,
          TELEM_ANA_A3,
          TELEM_ANA_A4,
          TELEM_ANA_COUNT
        }

        private static int _frskyRxBufferCount = 0;
        private static int _frskyStreaming = 0;
        private static int _link_counter = 0;

        private static byte[] _frskyRxBuffer;

        private const byte _StartStop = 0x7e;
        private const byte _ByteStuff = 0x7d;
        private const byte _StuffMask = 0x20;

        private const int _frskyRxPacketSize = 9;

        private const int _frskyTimeout10ms = 100;   // 1 second
        private const int _wshhTimeout10ms = 60;     // 600ms 
        private const int _frskySportAveraging = 4;
        private const int _frskyDAveraging = 8;

        // FrSky PRIM IDs (1 byte)
        private  const byte _DataFrame = 0x10;

        // FrSky old DATA IDs (1 byte)
        public const byte GPS_ALT_BP_ID = 0x01;
        public const byte TEMP1_ID = 0x02;
        public const byte RPM_ID = 0x03;
        public const byte FUEL_ID = 0x04;
        public const byte TEMP2_ID = 0x05;
        public const byte VOLTS_ID = 0x06;
        public const byte GPS_ALT_AP_ID = 0x09;
        public const byte BARO_ALT_BP_ID = 0x10;
        public const byte GPS_SPEED_BP_ID = 0x11;
        public const byte GPS_LONG_BP_ID = 0x12;
        public const byte GPS_LAT_BP_ID = 0x13;
        public const byte GPS_COURS_BP_ID = 0x14;
        public const byte GPS_DAY_MONTH_ID = 0x15;
        public const byte GPS_YEAR_ID = 0x16;
        public const byte GPS_HOUR_MIN_ID = 0x17;
        public const byte GPS_SEC_ID = 0x18;
        public const byte GPS_SPEED_AP_ID = 0x19;
        public const byte GPS_LONG_AP_ID = 0x1a;
        public const byte GPS_LAT_AP_ID = 0x1b;
        public const byte GPS_COURS_AP_ID = 0x1c;
        public const byte BARO_ALT_AP_ID = 0x21;
        public const byte GPS_LONG_EW_ID = 0x22;
        public const byte GPS_LAT_NS_ID = 0x23;
        public const byte ACCEL_X_ID = 0x24;
        public const byte ACCEL_Y_ID = 0x25;
        public const byte ACCEL_Z_ID = 0x26;
        public const byte CURRENT_ID = 0x28;
        public const byte VARIO_ID = 0x30;
        public const byte VFAS_ID = 0x39;
        public const byte VOLTS_BP_ID = 0x3a;
        public const byte VOLTS_AP_ID = 0x3b;
        public const byte FRSKY_LAST_ID = 0x3f;

        // FrSky new DATA IDs (2 bytes)
        public const UInt16 ALT_FIRST_ID = 0x0100;
        public const UInt16 ALT_LAST_ID = 0x010f;
        public const UInt16 VARIO_FIRST_ID = 0x0110;
        public const UInt16 VARIO_LAST_ID = 0x011f;
        public const UInt16 CURR_FIRST_ID = 0x0200;
        public const UInt16 CURR_LAST_ID = 0x020f;
        public const UInt16 VFAS_FIRST_ID = 0x0210;
        public const UInt16 VFAS_LAST_ID = 0x021f;
        public const UInt16 CELLS_FIRST_ID = 0x0300;
        public const UInt16 CELLS_LAST_ID = 0x0300f;
        public const UInt16 T1_FIRST_ID = 0x0400;
        public const UInt16 T1_LAST_ID = 0x040f;
        public const UInt16 T2_FIRST_ID = 0x0410;
        public const UInt16 T2_LAST_ID = 0x041f;
        public const UInt16 RPM_FIRST_ID = 0x0500;
        public const UInt16 RPM_LAST_ID = 0x050f;
        public const UInt16 FUEL_FIRST_ID = 0x0600;
        public const UInt16 FUEL_LAST_ID = 0x060f;
        public const UInt16 ACCX_FIRST_ID = 0x0700;
        public const UInt16 ACCX_LAST_ID = 0x070f;
        public const UInt16 ACCY_FIRST_ID = 0x0710;
        public const UInt16 ACCY_LAST_ID = 0x071f;
        public const UInt16 ACCZ_FIRST_ID = 0x0720;
        public const UInt16 ACCZ_LAST_ID = 0x072f;
        public const UInt16 GPS_LONG_LATI_FIRST_ID = 0x0800;
        public const UInt16 GPS_LONG_LATI_LAST_ID = 0x080f;
        public const UInt16 GPS_ALT_FIRST_ID = 0x0820;
        public const UInt16 GPS_ALT_LAST_ID = 0x082f;
        public const UInt16 GPS_SPEED_FIRST_ID = 0x0830;
        public const UInt16 GPS_SPEED_LAST_ID = 0x083f;
        public const UInt16 GPS_COURS_FIRST_ID = 0x0840;
        public const UInt16 GPS_COURS_LAST_ID = 0x084f;
        public const UInt16 GPS_TIME_DATE_FIRST_ID = 0x0850;
        public const UInt16 GPS_TIME_DATE_LAST_ID = 0x085f;
        public const UInt16 A3_FIRST_ID = 0x0900;
        public const UInt16 A3_LAST_ID = 0x090f;
        public const UInt16 A4_FIRST_ID = 0x0910;
        public const UInt16 A4_LAST_ID = 0x091f;
        public const UInt16 AIR_SPEED_FIRST_ID = 0x0a00;
        public const UInt16 AIR_SPEED_LAST_ID = 0x0a0f;
        public const UInt16 RSSI_ID = 0xf101;
        public const UInt16 ADC1_ID = 0xf102;
        public const UInt16 ADC2_ID = 0xf103;
        public const UInt16 BATT_ID = 0xf104;
        public const UInt16 SWR_ID = 0xf105;

        // Default sensor data IDs (Physical IDs + CRC)
        public const byte DATA_ID_VARIO = 0x00; // 0
        public const byte DATA_ID_FLVSS = 0xA1; // 1
        public const byte DATA_ID_FAS = 0x22; // 2
        public const byte DATA_ID_GPS = 0x83; // 3
        public const byte DATA_ID_RPM = 0xE4; // 4
        public const byte DATA_ID_SP2UH = 0x45; // 5
        public const byte DATA_ID_SP2UR = 0xC6; // 6

        public const UInt32 EARTH_RADIUSKM = 6371;
        public const UInt32 EARTH_RADIUS = 111194;  // 17 times EARTH_RADIUSKM, for some geometry reason probably having to do with degrees2radians
        public static int TelemetryAverageCount = 3;

        public static IFrskyTelemetryData DependentViewModel;

        private static FrSkyDataState _dataState = FrSkyDataState.STATE_DATA_IDLE;

        // Combines two arrays of Type T and returns a new array of Type T and
        // returns null if both arrays are null.  This will really freak out the embedded 'C' guys.
        // Don't worry, you don't have to explicitly keep track of this memory, it automatically 
        // gets released when all refs to it are null.
        public static T[] Combine<T>(T[] first, T[] second)
        {
            if(null != first && null != second)
            {
                T[] ret = new T[first.Length + second.Length];
                Buffer.BlockCopy(first, 0, ret, 0, first.Length);
                Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
                return ret;
            }
            else if (null == first && null != second)
            {
                T[] ret = new T[second.Length];
                Buffer.BlockCopy(second, 0, ret, 0, second.Length);
                return ret;
            }
            else if (null == second && null != first)
            {
                T[] ret = new T[first.Length];
                Buffer.BlockCopy(first, 0, ret, 0, first.Length);
                return ret;
            }

            return null;
        }

        // process the serial packet data read from Taranis until
        // a complete packet is formed, then decode
        public static void ProcessSerialData(byte[] data)
        {
            //log.DebugFormat("In ProcessSerialData, data={0}", data);
            //log.DebugFormat("DataState = {0}", _dataState.ToString());

            //string str;

            switch(_dataState)
            {
                case FrSkyDataState.STATE_DATA_START:
                    {
                        //log.Debug("In STATE_DATA_START case:");

                        if(_StartStop == data[0])
                        {
                            _dataState = FrSkyDataState.STATE_DATA_IN_FRAME;
                            _frskyRxBufferCount = 0;
                            _frskyRxBuffer = null;
          
                            //log.Debug("data[0] was StartStop byte.");
                        }
                        else if (_frskyRxBufferCount < _frskyRxPacketSize)
                        {
                            if (_frskyRxBufferCount > 0)
                            {
                                //log.DebugFormat("_frskyRxBuffer length is {0}", _frskyRxBuffer.Length);

                                //str = BitConverter.ToString(_frskyRxBuffer);
                                
                                //log.DebugFormat("_frskyRxBuffer before combine={0}", str);
                            }
                            

                            _dataState = FrSkyDataState.STATE_DATA_IN_FRAME;
                            _frskyRxBuffer = Combine<byte>(_frskyRxBuffer, data);
                            _frskyRxBufferCount = _frskyRxBuffer.Length;

                            //log.DebugFormat("_frskyRxBuffer length after combine is {0}", _frskyRxBuffer.Length);

                            //str = BitConverter.ToString(_frskyRxBuffer);
                            //log.DebugFormat("_frskyRxBuffer after combine={0}", str);
                        }
                        break;
                    }

                case FrSkyDataState.STATE_DATA_IN_FRAME:
                    {
                        //log.Debug("In STATE_DATA_IN_FRAME case:");

                        if (_ByteStuff == data[0])
                        {
                            //log.Debug("data[0] was Bytestuff byte, state changed to XOR.");

                            _dataState = FrSkyDataState.STATE_DATA_XOR;  // XOR next byte

                        }
                        else if (_StartStop == data[0])
                        {
                            //log.Debug("data[0] was StartStop byte, state changed to STATE_DATA_IN_FRAME.");

                            _dataState = FrSkyDataState.STATE_DATA_IN_FRAME;

                            _frskyRxBufferCount = 0;
                            _frskyRxBuffer = null;
                        }
                        else if (_frskyRxBufferCount < _frskyRxPacketSize)
                        {

                            //if(_frskyRxBufferCount > 0)
                            //{
                            //    //log.DebugFormat("_frskyRxBuffer length is {0}", _frskyRxBuffer.Length);
                            //   // str = BitConverter.ToString(_frskyRxBuffer);

                            //   // log.DebugFormat("_frskyRxBuffer before combine={0}", str);
                            //}


                            _frskyRxBuffer = Combine<byte>(_frskyRxBuffer, data);
                            _frskyRxBufferCount = _frskyRxBuffer.Length;

                            //log.DebugFormat("_frskyRxBuffer length after combine is {0}", _frskyRxBuffer.Length);

                            //str = BitConverter.ToString(_frskyRxBuffer);

                            //log.DebugFormat("_frskyRxBuffer after combine={0}", str);
                        }
                        break;
                    }

                case FrSkyDataState.STATE_DATA_XOR:
                    {
                        //log.Debug("In STATE_DATA_XOR case:");

                        if (_frskyRxBufferCount < _frskyRxPacketSize)
                        {
                            //if (_frskyRxBufferCount > 0)
                            //{
                            //    log.DebugFormat("_frskyRxBuffer length is {0}", _frskyRxBuffer.Length);

                            //    str = BitConverter.ToString(_frskyRxBuffer);
                            //    log.DebugFormat("_frskyRxBuffer before combine={0}", str);
                            //}

                            data[0] ^= _StuffMask;
                            _frskyRxBuffer = Combine<byte>(_frskyRxBuffer, data);
                            _frskyRxBufferCount = _frskyRxBuffer.Length;
                            _dataState = FrSkyDataState.STATE_DATA_IN_FRAME;

                            //log.DebugFormat("_frskyRxBuffer length after combine is {0}", _frskyRxBuffer.Length);

                            //str = BitConverter.ToString(_frskyRxBuffer);
                            //log.DebugFormat("_frskyRxBuffer after combine={0}", str);
                        }
                        break;
                    }

                case FrSkyDataState.STATE_DATA_IDLE:
                    {
                        //log.Debug("In STATE_DATA_IDLE case:");

                        if(_StartStop == data[0])
                        {
                            _frskyRxBufferCount = 0;
                            _frskyRxBuffer = null;
                            _dataState = FrSkyDataState.STATE_DATA_START;


                            //log.Debug("data[0] was StartStop byte. DataState changed to STATE_DATA_START.");
                        }

                        break;
                    }
            }

            if(_frskyRxBufferCount >= _frskyRxPacketSize)
            {
               // string str = BitConverter.ToString(_frskyRxBuffer);
               //log.DebugFormat("Before frskySportProcessPacket:  _frskyRxBufferCount = {0}, _frskyRxPacketSize = {1}", _frskyRxBufferCount, _frskyRxPacketSize);
               // log.DebugFormat("_frskyRxBuffer={0}", str);
                frskySportProcessPacket(_frskyRxBuffer);
                _dataState = FrSkyDataState.STATE_DATA_IDLE;
            }
        }

        // compute the CRC value of the packet
        private static bool checkSportPacket(byte[] packet)
        {
            short crc = 0;
            for(int i=1; i < _frskyRxPacketSize; i++)
            {
                crc += packet[i]; // 0-1ff
                crc +=(short)( crc >> 8);  // 0-100
                crc &= 0x00ff;
                crc += (short)(crc >> 8);  // 0-0ff
                crc &= 0x00ff;
            }

            return (crc == 0x00ff);
        }

        private static void frskySportProcessPacket(byte[] packet)
        {
            byte dataId = packet[0];
            byte prim = packet[1];
            UInt16 appId = BitConverter.ToUInt16(packet, 2);

            if (!checkSportPacket(packet))
            {
                return;
            }

            switch(prim)
            {
                case _DataFrame:
                    {
                        if (RSSI_ID == appId)
                        {
                            // this doesn't get used anywhere it seems.
                            _frskyStreaming = _frskyTimeout10ms;

                            _link_counter += 256 / _frskySportAveraging;

                            FrskyData.rssi[0].set(packet[4]);

                            // update the injected view model, if present
                            if(null != DependentViewModel)
                            {
                                DependentViewModel.RSSI = (int)(FrskyData.rssi[0].value);
                                DependentViewModel.RSSI_MIN = (int)(FrskyData.rssi[0].min);
                            }
                        }

                        if (SWR_ID == appId)
                        {
                            FrskyData.swr.set(packet[4]);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.SWR = (int)(FrskyData.swr.value);
                                DependentViewModel.SWR_MIN = (int)(FrskyData.swr.min);
                            }
                        }
                        else if (ADC1_ID == appId || ADC2_ID == appId)
                        {
                            // A1/A2 of DxR receivers
                            byte idx = (byte)(appId - ADC1_ID);

                            // avoid the unhandled exception for now if idx is wonky
                            if (idx < FrskyData.analog.Length && idx >= 0)
                            {
                                FrskyData.analog[idx].set(packet[4]);

                                // update the injected view model, if present
                                if (null != DependentViewModel)
                                {
                                    // the indexes may have to be adjusted after testing, 
                                    // I have no idea if this is right.
                                    if (0 == idx)
                                    {
                                        DependentViewModel.ADC1 = (int)(FrskyData.analog[idx].value);
                                        DependentViewModel.ADC1_MAX = (int)(FrskyData.analog[idx].max);
                                        DependentViewModel.ADC1_MIN = (int)(FrskyData.analog[idx].min);
                                    }
                                    else if (1 == idx)
                                    {
                                        DependentViewModel.ADC2 = (int)(FrskyData.analog[idx].value);
                                        DependentViewModel.ADC2_MAX = (int)(FrskyData.analog[idx].max);
                                        DependentViewModel.ADC2_MIN = (int)(FrskyData.analog[idx].min);
                                    }
                                }

                            }
                        }
                        else if (BATT_ID == appId)
                        {
                            FrskyData.analog[(int)TelemAnas.TELEM_ANA_A1].set(packet[4]);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.BATT_ID = (int)(FrskyData.analog[(int)TelemAnas.TELEM_ANA_A1].value);
                            }
                        }
                        else if ((appId >> 8) == 0)
                        {
                            // the old FrSky IDs
                            UInt16 value = BitConverter.ToUInt16(packet, 4);
                            byte id = (byte)appId;

                            // haha, don't forget to implement this later!  "NASA code comment, 1975 or so"
                            processHubPacket(id, value);
                        }
                        else if (appId >= T1_FIRST_ID && appId <= T1_LAST_ID)
                        {
                            // I hate this kind of cast. This must be a result of some CPU compatability madness.
                            FrskyData.hub.temperature1 = (Int16)BitConverter.ToInt32(packet, 4);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.TEMPERATURE1 = (int)(FrskyData.hub.temperature1);
                            }

                            if (FrskyData.hub.temperature1 > FrskyData.hub.maxTemperature1)
                            {
                                FrskyData.hub.maxTemperature1 = FrskyData.hub.temperature1;

                                // update the injected view model, if present
                                if (null != DependentViewModel)
                                {
                                    DependentViewModel.TEMPERATURE1_MAX = (int)(FrskyData.hub.maxTemperature1);
                                }
                            }
                        }
                        else if (appId >= T2_FIRST_ID && appId <= T2_LAST_ID)
                        {
                            // I hate this kind of cast
                            FrskyData.hub.temperature2 = (Int16)BitConverter.ToInt32(packet, 4);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.TEMPERATURE2 = (int)(FrskyData.hub.temperature2);
                            }

                            if (FrskyData.hub.temperature2 > FrskyData.hub.maxTemperature2)
                            {
                                FrskyData.hub.maxTemperature2 = FrskyData.hub.temperature2;

                                // update the injected view model, if present
                                if (null != DependentViewModel)
                                {
                                    DependentViewModel.TEMPERATURE2_MAX = (int)(FrskyData.hub.maxTemperature2);
                                }
                            }
                        }
                        else if (appId >= RPM_FIRST_ID && appId <= RPM_LAST_ID)
                        {
                            // Note: In the Taranis source, Frsky_sport.cpp, the quantity in the 
                            // packet is divided by the g_model.frsky.blades + 2. Since I have no idea how 
                            // many blades you have, I'm going to assume 1 for raw engine RPM, and thus,
                            // divide this by 3.  Adjust as necessary, Frsky experts.
                            FrskyData.hub.rpm = (UInt16)(BitConverter.ToInt32(packet, 4) / 3);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.RPM = (int)(FrskyData.hub.rpm);
                            }

                            if (FrskyData.hub.rpm > FrskyData.hub.maxRpm)
                            {
                                FrskyData.hub.maxRpm = FrskyData.hub.rpm;

                                // update the injected view model, if present
                                if (null != DependentViewModel)
                                {
                                    DependentViewModel.RPM_MAX = (int)(FrskyData.hub.maxRpm);
                                }
                            }
                        }
                        else if (appId >= FUEL_FIRST_ID && appId <= FUEL_LAST_ID)
                        {
                            FrskyData.hub.fuelLevel = (UInt16)BitConverter.ToInt32(packet, 4);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.FUEL = (int)(FrskyData.hub.fuelLevel);
                            }
                        }
                        else if (appId >= ALT_FIRST_ID && appId <= ALT_LAST_ID)
                        {
                            setBaroAltitude(BitConverter.ToInt32(packet, 4));

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.BARO_ALTITUDE = (int)(FrskyData.hub.baroAltitude);
                                DependentViewModel.BARO_ALTITUDE_MAX = (int)(FrskyData.hub.maxAltitude);
                                DependentViewModel.BARO_ALTITUDE_MIN = (int)(FrskyData.hub.minAltitude);
                            }
                        }
                        else if (appId >= VARIO_FIRST_ID && appId <= VARIO_LAST_ID)
                        {
                            FrskyData.hub.varioSpeed = (Int16)BitConverter.ToInt32(packet, 4);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.VARIO_SPEED = (int)(FrskyData.hub.varioSpeed);
                            }
                        }
                        else if (appId >= ACCX_FIRST_ID && appId <= ACCX_LAST_ID)
                        {
                            FrskyData.hub.accelX = (Int16)BitConverter.ToInt32(packet, 4);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.ACCLX = (int)(FrskyData.hub.accelX);
                            }
                        }
                        else if (appId >= ACCY_FIRST_ID && appId <= ACCY_LAST_ID)
                        {
                            FrskyData.hub.accelY = (Int16)BitConverter.ToInt32(packet, 4);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.ACCLY = (int)(FrskyData.hub.accelY);
                            }
                        }
                        else if (appId >= ACCZ_FIRST_ID && appId <= ACCZ_LAST_ID)
                        {
                            FrskyData.hub.accelZ = (Int16)BitConverter.ToInt32(packet, 4);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.ACCLZ = (int)(FrskyData.hub.accelZ);
                            }
                        }
                        else if (appId >= CURR_FIRST_ID && appId <= CURR_LAST_ID)
                        {
                            FrskyData.hub.current = (UInt16)BitConverter.ToInt32(packet, 4);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.CURRENT = (int)(FrskyData.hub.current);
                            }

                            // We've got no access to g_model on the PC
                            //
                            //if ((FrskyData.hub.current + g_model.frsky.fasOffset) > 0)
                            //    frskyData.hub.current += g_model.frsky.fasOffset;
                            //else
                            //    frskyData.hub.current = 0;

                            if (FrskyData.hub.current > FrskyData.hub.maxCurrent)
                            {
                                FrskyData.hub.maxCurrent = FrskyData.hub.current;

                                // update the injected view model, if present
                                if (null != DependentViewModel)
                                {
                                    DependentViewModel.CURRENT_MAX = (int)(FrskyData.hub.maxCurrent);
                                }
                            }
                        }
                        else if (appId >= VFAS_FIRST_ID && appId <= VFAS_LAST_ID)
                        {
                            FrskyData.hub.vfas = (UInt16)(BitConverter.ToInt32(packet, 4) / 10);   //TODO: remove /10 and display with PREC2 when using SPORT

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.VFAS = (int)(FrskyData.hub.vfas);
                            }

                            if (0 == FrskyData.hub.minVfas || FrskyData.hub.vfas < FrskyData.hub.minVfas)
                            {
                                FrskyData.hub.minVfas = (Int16)FrskyData.hub.vfas;

                                // update the injected view model, if present
                                if (null != DependentViewModel)
                                {
                                    DependentViewModel.VFAS_MIN = (int)(FrskyData.hub.minVfas);
                                }
                            }
                        }
                        else if (appId >= AIR_SPEED_FIRST_ID && appId <= AIR_SPEED_LAST_ID)
                        {
                            FrskyData.hub.airSpeed = (UInt16)(BitConverter.ToInt32(packet, 4));

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.AIRSPEED = (int)(FrskyData.hub.airSpeed);
                            }

                            if (FrskyData.hub.airSpeed > FrskyData.hub.maxAirSpeed)
                            {
                                FrskyData.hub.maxAirSpeed = FrskyData.hub.airSpeed;

                                // update the injected view model, if present
                                if (null != DependentViewModel)
                                {
                                    DependentViewModel.AIRSPEED_MAX = (int)(FrskyData.hub.maxAirSpeed);
                                }
                            }
                        }
                        else if (appId >= GPS_SPEED_FIRST_ID && appId <= GPS_SPEED_LAST_ID)
                        {
                            FrskyData.hub.gpsSpeed_bp = (UInt16)((BitConverter.ToInt32(packet, 4) / 1000));

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.GPS_SPEED_BP = (int)(FrskyData.hub.gpsSpeed_bp);
                            }

                            if (FrskyData.hub.gpsSpeed_bp > FrskyData.hub.maxGpsSpeed)
                            {
                                FrskyData.hub.maxGpsSpeed = FrskyData.hub.gpsSpeed_bp;

                                // update the injected view model, if present
                                if (null != DependentViewModel)
                                {
                                    DependentViewModel.GPS_SPEED_MAX = (int)(FrskyData.hub.maxGpsSpeed);
                                }
                            }
                        }
                        else if (appId >= GPS_TIME_DATE_FIRST_ID && appId <= GPS_TIME_DATE_LAST_ID)
                        {
                            UInt32 gps_time_date = (UInt32)((BitConverter.ToInt32(packet, 4)));

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.GPS_DATE_TIME = gps_time_date;
                            }

                            if ((gps_time_date & 0x000000ff) != 0)
                            {
                                FrskyData.hub.year = (UInt16)((gps_time_date & 0xff000000) >> 24);
                                FrskyData.hub.month = (byte)((gps_time_date & 0x00ff0000) >> 16);
                                FrskyData.hub.day = (byte)((gps_time_date & 0x0000ff00) >> 8);

                                // update the injected view model, if present
                                if (null != DependentViewModel)
                                {
                                    DependentViewModel.GPS_YEAR = (int)(FrskyData.hub.year);
                                    DependentViewModel.GPS_MONTH = (int)(FrskyData.hub.month);
                                    DependentViewModel.GPS_DAY = (int)(FrskyData.hub.day);
                                }

                            }
                            else
                            {
                                FrskyData.hub.hour = (byte)((gps_time_date & 0xff000000) >> 24);
                                FrskyData.hub.min = (byte)((gps_time_date & 0x00ff0000) >> 16);
                                FrskyData.hub.sec = (UInt16)((gps_time_date & 0x0000ff00) >> 8);

                                // update the injected view model, if present
                                if (null != DependentViewModel)
                                {
                                    DependentViewModel.GPS_HOUR = (int)(FrskyData.hub.hour);
                                    DependentViewModel.GPS_MINUTE = (int)(FrskyData.hub.min);
                                    DependentViewModel.GPS_SECOND = (int)(FrskyData.hub.sec);
                                }


                                // Don't know what g_eeGeneral is, don't know how to make it.
                                // FrskyData.hub.hour = ((uint8_t)(frskyData.hub.hour + g_eeGeneral.timezone + 24)) % 24;
                            }
                        }
                        else if (appId >= GPS_COURS_FIRST_ID && appId <= GPS_COURS_LAST_ID)
                        {
                            UInt32 course = BitConverter.ToUInt32(packet, 4);

                            FrskyData.hub.gpsCourse_bp = (UInt16)(course / 100);
                            FrskyData.hub.gpsCourse_ap = (UInt16)(course % 100);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.GPS_COURSE_BP = (int)(FrskyData.hub.gpsCourse_bp);
                                DependentViewModel.GPS_COURSE_AP = (int)(FrskyData.hub.gpsCourse_ap);
                            }

                        }
                        else if (appId >= GPS_ALT_FIRST_ID && appId <= GPS_ALT_LAST_ID)
                        {
                            FrskyData.hub.gpsAltitude = BitConverter.ToInt32(packet, 4);

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.GPS_ALTITUDE = (int)(FrskyData.hub.gpsAltitude);
                            }


                            if (0 == FrskyData.hub.gpsAltitudeOffset)
                            {
                                FrskyData.hub.gpsAltitudeOffset = -FrskyData.hub.gpsAltitude;

                                // update the injected view model, if present
                                if (null != DependentViewModel)
                                {
                                    DependentViewModel.GPS_ALTITUDE_OFFSET = (int)(FrskyData.hub.gpsAltitudeOffset);
                                }
                            }


                            if (0 == FrskyData.hub.baroAltitudeOffset)
                            {
                                int altitude = FrskyData.hub.gpsAltitude_bp;

                                if (altitude > FrskyData.hub.maxAltitude)
                                {
                                    FrskyData.hub.maxAltitude = (Int16)altitude;

                                    // update the injected view model, if present
                                    if (null != DependentViewModel)
                                    {
                                        DependentViewModel.MAX_ALTITUDE = (int)(FrskyData.hub.maxAltitude);
                                    }
                                }

                                if (altitude < FrskyData.hub.minAltitude)
                                {
                                    FrskyData.hub.minAltitude = (Int16)altitude;

                                    // update the injected view model, if present
                                    if (null != DependentViewModel)
                                    {
                                        DependentViewModel.MIN_ALTITUDE = (int)(FrskyData.hub.minAltitude);
                                    }
                                }

                            }

                            if (FrskyData.hub.gpsFix > 0)
                            {
                                if (0 == FrskyData.hub.pilotLatitude && 0 == FrskyData.hub.pilotLongitude)
                                {
                                    // First received GPS position => Pilot GPS position
                                    // DependentViewModel updated in called function
                                    getGpsPilotPosition();
                                }
                                else if (0 == FrskyData.hub.gpsDistNeeded)
                                {
                                    // DependentViewModel updated in called function
                                    getGpsDistance();
                                }
                            }
                        }
                        else if (appId >= GPS_LONG_LATI_FIRST_ID && appId <= GPS_LONG_LATI_LAST_ID)
                        {
                            UInt32 gps_long_lati_data = BitConverter.ToUInt32(packet, 4);
                            UInt32 gps_long_lati_b1w, gps_long_lati_a1w;
                            gps_long_lati_b1w = (gps_long_lati_data & 0x3fffffff) / 10000;
                            gps_long_lati_a1w = (gps_long_lati_data & 0x3fffffff) % 10000;

                            switch ((gps_long_lati_data & 0xc0000000) >> 30)
                            {
                                case 0:
                                    FrskyData.hub.gpsLatitude_bp = (UInt16)((gps_long_lati_b1w / 60 * 100) + (gps_long_lati_b1w % 60));
                                    FrskyData.hub.gpsLatitude_ap = (UInt16)gps_long_lati_a1w;
                                    FrskyData.hub.gpsLatitudeNS = "N";

                                    // update the injected view model, if present
                                    if (null != DependentViewModel)
                                    {
                                        DependentViewModel.GPS_LATITUDE_BP = (int)(FrskyData.hub.gpsLatitude_bp);
                                        DependentViewModel.GPS_LATITUDE_AP = (int)(FrskyData.hub.gpsLatitude_ap);
                                        DependentViewModel.GPS_LATITUDE_NS = (string)FrskyData.hub.gpsLatitudeNS;
                                    }

                                    break;
                                case 1:
                                    FrskyData.hub.gpsLatitude_bp = (UInt16)((gps_long_lati_b1w / 60 * 100) + (gps_long_lati_b1w % 60));
                                    FrskyData.hub.gpsLatitude_ap = (UInt16)gps_long_lati_a1w;
                                    FrskyData.hub.gpsLatitudeNS = "S";
                                    
                                    // update the injected view model, if present
                                    if (null != DependentViewModel)
                                    {
                                        DependentViewModel.GPS_LATITUDE_BP = (int)(FrskyData.hub.gpsLatitude_bp);
                                        DependentViewModel.GPS_LATITUDE_AP = (int)(FrskyData.hub.gpsLatitude_ap);
                                        DependentViewModel.GPS_LATITUDE_NS = (string)FrskyData.hub.gpsLatitudeNS;
                                    }

                                    break;
                                case 2:
                                    FrskyData.hub.gpsLongitude_bp = (UInt16)((gps_long_lati_b1w / 60 * 100) + (gps_long_lati_b1w % 60));
                                    FrskyData.hub.gpsLongitude_ap = (UInt16)gps_long_lati_a1w;
                                    FrskyData.hub.gpsLongitudeEW = "E";

                                    // update the injected view model, if present
                                    if (null != DependentViewModel)
                                    {
                                        DependentViewModel.GPS_LONGITUDE_BP = (int)(FrskyData.hub.gpsLongitude_bp);
                                        DependentViewModel.GPS_LONGITUDE_AP = (int)(FrskyData.hub.gpsLongitude_ap);
                                        DependentViewModel.GPS_LONGITUDE_EW = (string)FrskyData.hub.gpsLongitudeEW;
                                    }

                                    break;
                                case 3:
                                    FrskyData.hub.gpsLongitude_bp = (UInt16)((gps_long_lati_b1w / 60 * 100) + (gps_long_lati_b1w % 60));
                                    FrskyData.hub.gpsLongitude_ap = (UInt16)gps_long_lati_a1w;
                                    FrskyData.hub.gpsLongitudeEW = "W";

                                    // update the injected view model, if present
                                    if (null != DependentViewModel)
                                    {
                                        DependentViewModel.GPS_LONGITUDE_BP = (int)(FrskyData.hub.gpsLongitude_bp);
                                        DependentViewModel.GPS_LONGITUDE_AP = (int)(FrskyData.hub.gpsLongitude_ap);
                                        DependentViewModel.GPS_LONGITUDE_EW = (string)FrskyData.hub.gpsLongitudeEW;
                                    }

                                    break;
                            }

                            if((FrskyData.hub.gpsLongitudeEW.StartsWith("E") || FrskyData.hub.gpsLongitudeEW.StartsWith("W") &&
                               (FrskyData.hub.gpsLatitudeNS.StartsWith("N")  || FrskyData.hub.gpsLatitudeNS.StartsWith("S"))))
                            {
                                FrskyData.hub.gpsFix = 1;
                            }
                            else if (FrskyData.hub.gpsFix > 0)
                            {
                                FrskyData.hub.gpsFix = 0;
                            }

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.GPS_FIX = (int)FrskyData.hub.gpsFix;
                            }
                        }
                        else if (appId >= A3_FIRST_ID && appId <= A3_LAST_ID)
                        {
                            // taking a 32-bit value and putting it into a byte. sheesh. 
                            FrskyData.analog[(int)TelemAnas.TELEM_ANA_A3].set((Byte)((BitConverter.ToUInt32(packet, 4) * 255 + 165) / 330));

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                    DependentViewModel.ADC3 = (int)(FrskyData.analog[(int)TelemAnas.TELEM_ANA_A3].value);
                                    DependentViewModel.ADC3_MAX = (int)(FrskyData.analog[(int)TelemAnas.TELEM_ANA_A3].max);
                                    DependentViewModel.ADC3_MIN = (int)(FrskyData.analog[(int)TelemAnas.TELEM_ANA_A3].min);
                            }
                        }
                        else if (appId >= A4_FIRST_ID && appId <= A4_LAST_ID)
                        {
                            // ditto.
                            FrskyData.analog[(int)TelemAnas.TELEM_ANA_A4].set((Byte)((BitConverter.ToUInt32(packet, 4) * 255 + 165) / 330));

                            // update the injected view model, if present
                            if (null != DependentViewModel)
                            {
                                DependentViewModel.ADC4 = (int)(FrskyData.analog[(int)TelemAnas.TELEM_ANA_A4].value);
                                DependentViewModel.ADC4_MAX = (int)(FrskyData.analog[(int)TelemAnas.TELEM_ANA_A4].max);
                                DependentViewModel.ADC4_MIN = (int)(FrskyData.analog[(int)TelemAnas.TELEM_ANA_A4].min);
                            }
                        }
                        else if (appId >= CELLS_FIRST_ID && appId <= CELLS_LAST_ID)
                        {
                            UInt32 data = BitConverter.ToUInt32(packet, 4);
                            byte battnumber = (byte)(data & 0xF);
                            byte cells = (byte)((data & 0xF0) >> 4);
                            bool useSecondCell = (battnumber + 1 < cells);

                            log.DebugFormat("battnumber={0}, cells={1}, useSecondCell={2}, data={3}", battnumber, cells, useSecondCell, data);

                            if (dataId == DATA_ID_FLVSS)
                            {

                                // first sensor, remember its cell count
                                FrskyData.hub.sensorCellsCount[0] = cells;
                                cells += FrskyData.hub.sensorCellsCount[1];

                                log.DebugFormat("dataId = DATA_ID_FLVSS, cells={0}", cells);
                            }
                            else
                            {
                                // second sensor connected
                                FrskyData.hub.sensorCellsCount[1] = cells;
                                cells += FrskyData.hub.sensorCellsCount[0];
                                battnumber += FrskyData.hub.sensorCellsCount[0];

                                log.DebugFormat("seconds sensor connected, cells={0}, battnumber={1}", cells, battnumber);
                            }

                            if (cells != FrskyData.hub.cellsCount)
                            {
                                frskySetCellsCount(cells);
                            }

                            frskySetCellVoltage(battnumber, (UInt16)(((data & 0x000FFF00) >> 8) / 5));

                            if (useSecondCell)
                            {
                                frskySetCellVoltage((byte)(battnumber + 1), (UInt16)(((data & 0xFFF00000) >> 20) / 5));
                            }
                        }

                        break;
                    } // end case 
            }  // end switch prim

        }

        private static void frskySetCellsCount(byte cellscount)
        {
            if (cellscount <= FrskyData.hub.cellVolts.Length)
            {
                FrskyData.hub.cellsCount = cellscount;
                FrskyData.hub.cellsState = 0;
                FrskyData.hub.minCells = 0;
               FrskyData.hub.minCell = 0;
            }
        }

        private static void frskySetCellVoltage(byte battnumber, UInt16 cellVolts)
        {
            // TRACE("frskySetCellVoltage() %d, %d", battnumber, cellVolts);

            log.DebugFormat("frskySetCellVoltage, battNumber={0}, cellVolts={1}, cellsCount={2}", battnumber, cellVolts, FrskyData.hub.cellsCount);

            if (battnumber < FrskyData.hub.cellsCount)
            {
                // set cell voltage
                if (cellVolts > 50)  // Filter out bogus cell values apparently sent by the FLVSS in some cases
                {
                    FrskyData.hub.cellVolts[battnumber] = cellVolts;

                    // Because of the way .Net properties work, this is actually the most intuitive
                    // way of doing this for a UI designer, instead of an array or List object.
                    // Hey, but it's open source, right?  Change it if you want.
                    if(null != DependentViewModel)
                    {
                        if (0 == battnumber)
                        {
                            DependentViewModel.CELL1 = cellVolts;
                        }
                        else if (1 == battnumber)
                        {
                            DependentViewModel.CELL2 = cellVolts;
                        }
                        else if (2 == battnumber)
                        {
                            DependentViewModel.CELL3 = cellVolts;
                        }
                        else if (3 == battnumber)
                        {
                            DependentViewModel.CELL4 = cellVolts;
                        }
                        else if (4 == battnumber)
                        {
                            DependentViewModel.CELL5 = cellVolts;
                        }
                        else if (5 == battnumber)
                        {
                            DependentViewModel.CELL6 = cellVolts;
                        }
                        else if (6 == battnumber)
                        {
                            DependentViewModel.CELL7 = cellVolts;
                        }
                        else if (7 == battnumber)
                        {
                            DependentViewModel.CELL8 = cellVolts;
                        }
                        else if (8 == battnumber)
                        {
                            DependentViewModel.CELL9 = cellVolts;
                        }
                        else if (9 == battnumber)
                        {
                            DependentViewModel.CELL10 = cellVolts;
                        }
                        else if (10 == battnumber)
                        {
                            DependentViewModel.CELL11 = cellVolts;
                        }
                        else if (11 == battnumber)
                        {
                            DependentViewModel.CELL12 = cellVolts;
                        }
                        else
                        {
                            log.DebugFormat("Out of range battery number was {0}", battnumber);
                        }

                    }
                }
                    
                if (cellVolts != 0)
                {
                    FrskyData.hub.cellsState |= (UInt16)(1 << battnumber);

                    if (FrskyData.hub.cellsState == (1 << FrskyData.hub.cellsCount) - 1)
                    {
                        // we received voltage of all cells
                        FrskyData.hub.cellsState = 0;

                        // calculate Cells, Cells-, Cell and Cell-
                        UInt16 cellsSum = 0; /* unit: 1/10 volts */
                        Int16 minCellVolts = -1;
                        for (byte i = 0; i < FrskyData.hub.cellsCount; i++)
                        {
                            UInt16 tmpCellVolts = FrskyData.hub.cellVolts[i];
                            cellsSum += tmpCellVolts;
                            if (tmpCellVolts < minCellVolts)
                            {
                                // update minimum cell voltage (Cell)
                                minCellVolts = (Int16)tmpCellVolts;
                            }
                        }

                        FrskyData.hub.minCellVolts = (UInt16)minCellVolts;

                        //frskyData.hub.cellsSum = cellsSum / (10 / TELEMETRY_CELL_VOLTAGE_MUTLIPLIER);
                        FrskyData.hub.cellsSum = (Int16)(cellsSum / 10);

                        if(null != DependentViewModel)
                        {
                            DependentViewModel.CELLS_SUM = (int)FrskyData.hub.cellsSum;
                            DependentViewModel.CELL_VOLTS_MIN = (int)FrskyData.hub.minCellVolts;
                        }

                        // update cells sum minimum (Cells-)
                        if (0 == FrskyData.hub.minCells || FrskyData.hub.cellsSum < FrskyData.hub.minCells)
                        {
                            FrskyData.hub.minCells = FrskyData.hub.cellsSum;

                            if (null != DependentViewModel)
                            {
                                DependentViewModel.CELLS_SUM_MIN = (int)FrskyData.hub.minCells;
                            }
                        }

                        // update minimum cell voltage (Cell-)
                        if (0 == FrskyData.hub.minCell || FrskyData.hub.minCellVolts < FrskyData.hub.minCell)
                        {
                            FrskyData.hub.minCell = (Int16)FrskyData.hub.minCellVolts;

                            if (null != DependentViewModel)
                            {
                                DependentViewModel.MIN_CELL = (int)FrskyData.hub.minCell;
                            }
                        }
                    }
                }
            }
        }

        private static FrskyGeoLocation extractLatitudeLongitude()
        {
            UInt32 latitude;
            UInt32 longitude;
            int remainder=0;

            int quot = Math.DivRem(FrskyData.hub.gpsLatitude_bp, 100, out remainder);
            latitude = ((UInt32)(quot) * 1000000) + (((UInt32)(remainder) * 10000 + FrskyData.hub.gpsLatitude_ap) * 5) / 3;

            quot = Math.DivRem(FrskyData.hub.gpsLongitude_bp, 100, out remainder);
            longitude = ((UInt32)(quot) * 1000000) + (((UInt32)(remainder) * 10000 + FrskyData.hub.gpsLongitude_ap) * 5) / 3;

            return new FrskyGeoLocation(latitude, longitude);
        }

        private static void getGpsDistance()
        {
            FrskyGeoLocation loc = extractLatitudeLongitude();

            // printf("lat=%d (%d), long=%d (%d)\n", lat, abs(lat - frskyData.hub.pilotLatitude), lng, abs(lng - frskyData.hub.pilotLongitude));

            UInt32 angle = (loc.Lat > FrskyData.hub.pilotLatitude) ? loc.Lat - FrskyData.hub.pilotLatitude : FrskyData.hub.pilotLatitude - loc.Lat;
            UInt32 dist = EARTH_RADIUS * angle / 1000000;
            UInt32 result = dist * dist;

            angle = (loc.Lon > FrskyData.hub.pilotLongitude) ? loc.Lon - FrskyData.hub.pilotLongitude : FrskyData.hub.pilotLongitude - loc.Lon;
            dist = FrskyData.hub.distFromEarthAxis * angle / 1000000;
            result += dist * dist;

            // dist = abs(TELEMETRY_BARO_ALT_AVAILABLE() ? TELEMETRY_RELATIVE_BARO_ALT_BP : TELEMETRY_RELATIVE_GPS_ALT_BP);
            int tmp;
            if(0 == FrskyData.hub.baroAltitudeOffset)
            {
                tmp = FrskyData.hub.baroAltitude / 100;
            }
            else
            {
                tmp = (FrskyData.hub.gpsAltitude + FrskyData.hub.gpsAltitudeOffset) / 100;
            }
            dist = (UInt32)Math.Abs(tmp);
            result += dist * dist;

            FrskyData.hub.gpsDistance = isqrt32(result);

            if(null != DependentViewModel)
            {
                DependentViewModel.GPS_DISTANCE = (int)FrskyData.hub.gpsDistance;
            }

            if (FrskyData.hub.gpsDistance > FrskyData.hub.maxGpsDistance)
            {
                // I hate casts like this.
                FrskyData.hub.maxGpsDistance = (UInt16)FrskyData.hub.gpsDistance;

                if (null != DependentViewModel)
                {
                    DependentViewModel.GPS_DISTANCE_MAX = (int)FrskyData.hub.maxGpsDistance;
                }
            }
                
        }

        // http://www.codecodex.com/wiki/Calculate_an_integer_square_root
        public static UInt32 isqrt32 (UInt32 n) 
        {  
            UInt32  root, remainder, place;  
  
            root = 0;  
            remainder = n;  
            place = 0x40000000; // OR place = 0x4000; OR place = 0x40; - respectively  
  
            while (place > remainder)
            {
                place = place >> 2;
            }
                  
            while ( 0 != place)  
            {  
                if (remainder >= root + place)  
                {  
                    remainder = remainder - root - place;  
                    root = root + (place << 1);  
                }  
                root = root >> 1;  
                place = place >> 2;  
            }  
            return root;  
        }

        private static double gpsToDouble(bool neg, UInt16 bp, UInt16 ap)
        {
            double result = ap;
            result /= 10000;
            result += (bp % 100);
            result /= 60;
            result += (bp / 100);
            return neg ? -result : result;
        }

        private static void getGpsPilotPosition()
        {
            // these don't seem to be used for anything, what's up, Frsky experts?
            double pilotLatitude = gpsToDouble(FrskyData.hub.gpsLatitudeNS.StartsWith("S"), FrskyData.hub.gpsLatitude_bp, FrskyData.hub.gpsLatitude_ap);
            double pilotLongitude = gpsToDouble(FrskyData.hub.gpsLongitudeEW.StartsWith("W"), FrskyData.hub.gpsLongitude_bp, FrskyData.hub.gpsLongitude_ap);

            // extractLatitudeLongitude(&frskyData.hub.pilotLatitude, &frskyData.hub.pilotLongitude);
            FrskyGeoLocation loc = extractLatitudeLongitude();
            FrskyData.hub.pilotLatitude = loc.Lat;
            FrskyData.hub.pilotLongitude = loc.Lon;

            UInt32 lat = FrskyData.hub.pilotLatitude / 10000;
            UInt32 angle2 = (lat*lat) / 10000;
            UInt32 angle4 = angle2 * angle2;
            FrskyData.hub.distFromEarthAxis = 139*(((UInt32)10000000-((angle2*(UInt32)123370)/81)+(angle4/25))/12500);

            if (null != DependentViewModel)
            {
                DependentViewModel.PILOT_LATITUDE = (int)FrskyData.hub.pilotLatitude;
                DependentViewModel.PILOT_LONGITUDE = (int)FrskyData.hub.pilotLongitude;
                DependentViewModel.DIST_FROM_EARTH_AXIS = (int)FrskyData.hub.distFromEarthAxis;
            }

          // printf("frskyData.hub.distFromEarthAxis=%d\n", frskyData.hub.distFromEarthAxis); fflush(stdout);
        }

        private static void setBaroAltitude(int baroAltitude)
        {
            // First received barometer altitude => Altitude offset
            if (0 == FrskyData.hub.baroAltitudeOffset)
            {
                FrskyData.hub.baroAltitudeOffset = -baroAltitude;
            }
                
            baroAltitude += FrskyData.hub.baroAltitudeOffset;
            FrskyData.hub.baroAltitude = baroAltitude;

            baroAltitude /= 100;

            if (baroAltitude > FrskyData.hub.maxAltitude)
            {
                FrskyData.hub.maxAltitude = (Int16)baroAltitude;
            }
                
            if (baroAltitude < FrskyData.hub.minAltitude)
            {
                FrskyData.hub.minAltitude = (Int16)baroAltitude;
            }              
        }


        // to do after frskyProcessSportPacket is debugged, because
        // I don't have a hub or any hub sensors.
        private static void processHubPacket(byte id, UInt16 value)
        {

        }

    }
}
