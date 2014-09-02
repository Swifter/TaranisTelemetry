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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

using log4net;



namespace TaranisTelemetryListener
{
    class FrskyTelemetryViewModel : IFrskyTelemetryData, INotifyPropertyChanged
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FrskyTelemetryViewModel));

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string property)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(property));

            var value = this.GetType().GetProperty(property).GetValue(this, null);

            log.DebugFormat("Property Name:{0}, Property Value:{1}", property, value.ToString());
        }
        

        // properties backing store
        private int _rssi;
        private int _rssiMin;
        private int _swr;
        private int _swrMin;
        private int _adc1;
        private int _adc1Min;
        private int _adc1Max;
        private int _adc2;
        private int _adc2Min;
        private int _adc2Max;
        private int _adc3;
        private int _adc3Min;
        private int _adc3Max;
        private int _adc4;
        private int _adc4Min;
        private int _adc4Max;
        private int _battId;
        private int _temp1;
        private int _temp1Max;
        private int _temp2;
        private int _temp2Max;
        private int _rpm;
        private int _rpmMax;
        private int _fuel;
        private int _baroAltitude;
        private int _baroAltitudeMin;
        private int _baroAltitudeMax;
        private int _varioSpeed;
        private int _acclX;
        private int _acclY;
        private int _acclZ;
        private int _current;
        private int _currentMax;
        private int _vfas;
        private int _vfasMin;
        private int _airspeed;
        private int _airspeedMax;
        private int _gpsSpeedBP;
        private int _gpsSpeedMax;
        private UInt32 _gpsDateTime;
        private int _gpsYear;
        private int _gpsMonth;
        private int _gpsDay;
        private int _gpsHour;
        private int _gpsMinute;
        private int _gpsSecond;
        private int _gpsCourseAp;
        private int _gpsCourseBp;
        private int _cellsCount;
        private List<int> _sensorCellsCountList = new List<int>();
        private int _cellsSum;
        private int _cellsSumMin;
        private int _cellsState;
        private int _cellVoltsMin;
        private int _gpsAltitude;
        private int _gpsAltitudeBP;
        private int _gpsAltitudeOffset;
        private int _minAltitude;
        private int _maxAltitude;
        private int _pilotLatitude;
        private int _pilotLongitude;
        private int _distFromEarthAxis;
        private int _gpsDistance;
        private int _gpsDistanceMax;
        private int _gpsLatitudeBP;
        private int _gpsLatitudeAP;
        private int _gpsLongitudeBP;
        private int _gpsLongitudeAP;
        private string _gpsLongitudeEW;
        private string _gpsLatitudeNS;
        private int _gpsFix;
        private int _minCell;

        private int _cell1;
        private int _cell2;
        private int _cell3;
        private int _cell4;
        private int _cell5;
        private int _cell6;
        private int _cell7;
        private int _cell8;
        private int _cell9;
        private int _cell10;
        private int _cell11;
        private int _cell12;

        /// <summary>
        /// Radio Signal Strength Indicator.  
        /// </summary>
        public int RSSI
        {
            get
            {
                return _rssi;
            }

            set
            {
                if (value != _rssi)
                {
                    _rssi = value;
                    OnPropertyChanged("RSSI");
                }
            }
        }

        /// <summary>
        /// Minimum RSSI value for this session.
        /// </summary>
        public int RSSI_MIN
        {
            get
            {
                return _rssiMin;
            }

            set
            {
                if (value != _rssiMin)
                {
                    _rssiMin = value;
                    OnPropertyChanged("RSSI_MIN");
                }
            }
        }

        /// <summary>
        /// Standing Wave Ratio, percentage of the signal lost to standing waves?
        /// </summary>
        public int SWR
        {
            get
            {
                return _swr;
            }

            set
            {
                if (value != _swr)
                {
                    _swr = value;
                    OnPropertyChanged("SWR");
                }
            }
        }

        /// <summary>
        /// Minimum Standing Wave Ratio this session.
        /// </summary>
        public int SWR_MIN
        {
            get
            {
                return _swrMin;
            }

            set
            {
                if (value != _swrMin)
                {
                    _swrMin = value;
                    OnPropertyChanged("SWR_MIN");
                }
            }
        }

        public int ADC1
        {
            get
            {
                return _adc1;
            }

            set
            {
                if (value != _adc1)
                {
                    _adc1 = value;
                    OnPropertyChanged("ADC1");
                }
            }
        }

        public int ADC1_MIN
        {
            get
            {
                return _adc1Min;
            }

            set
            {
                if (value != _adc1Min)
                {
                    _adc1Min = value;
                    OnPropertyChanged("ADC1_MIN");
                }
            }
        }

        public int ADC1_MAX
        {
            get
            {
                return _adc1Max;
            }

            set
            {
                if (value != _adc1Max)
                {
                    _adc1Max = value;
                    OnPropertyChanged("ADC1_MAX");
                }
            }
        }

        public int ADC2
        {
            get
            {
                return _adc2;
            }

            set
            {
                if (value != _adc2)
                {
                    _adc2 = value;
                    OnPropertyChanged("ADC2");
                }
            }
        }

        public int ADC2_MIN
        {
            get
            {
                return _adc2Min;
            }

            set
            {
                if (value != _adc2Min)
                {
                    _adc2Min = value;
                    OnPropertyChanged("ADC2_MIN");
                }
            }
        }

        public int ADC2_MAX
        {
            get
            {
                return _adc2Max;
            }

            set
            {
                if (value != _adc2Max)
                {
                    _adc2Max = value;
                    OnPropertyChanged("ADC2_MAX");
                }
            }
        }

        public int ADC3
        {
            get
            {
                return _adc3;
            }

            set
            {
                if (value != _adc3)
                {
                    _adc3 = value;
                    OnPropertyChanged("ADC3");
                }
            }
        }

        public int ADC3_MIN
        {
            get
            {
                return _adc3Min;
            }

            set
            {
                if (value != _adc3Min)
                {
                    _adc3Min = value;
                    OnPropertyChanged("ADC3_MIN");
                }
            }
        }

        public int ADC3_MAX
        {
            get
            {
                return _adc3Max;
            }

            set
            {
                if (value != _adc3Max)
                {
                    _adc3Max = value;
                    OnPropertyChanged("ADC3_MAX");
                }
            }
        }

        public int ADC4
        {
            get
            {
                return _adc4;
            }

            set
            {
                if (value != _adc4)
                {
                    _adc4 = value;
                    OnPropertyChanged("ADC4");
                }
            }
        }

        public int ADC4_MIN
        {
            get
            {
                return _adc4Min;
            }

            set
            {
                if (value != _adc4Min)
                {
                    _adc4Min = value;
                    OnPropertyChanged("ADC4_MIN");
                }
            }
        }

        public int ADC4_MAX
        {
            get
            {
                return _adc4Max;
            }

            set
            {
                if (value != _adc4Max)
                {
                    _adc4Max = value;
                    OnPropertyChanged("ADC4_MAX");
                }
            }
        }

        public int BATT_ID
        {
            get
            {
                return _battId;
            }

            set
            {
                if (value != _battId)
                {
                    _battId = value;
                    OnPropertyChanged("BATT_ID");
                }
            }
        }

        public int TEMPERATURE1
        {
            get
            {
                return _temp1;
            }

            set
            {
                if (value != _temp1)
                {
                    _temp1 = value;
                    OnPropertyChanged("TEMPERATURE1");
                }
            }
        }

        public int TEMPERATURE1_MAX
        {
            get
            {
                return _temp1Max;
            }

            set
            {
                if (value != _temp1Max)
                {
                    _temp1Max = value;
                    OnPropertyChanged("TEMPERATURE1_MAX");
                }
            }
        }

        public int TEMPERATURE2
        {
            get
            {
                return _temp2;
            }

            set
            {
                if (value != _temp2)
                {
                    _temp2 = value;
                    OnPropertyChanged("TEMPERATURE2");
                }
            }
        }

        public int TEMPERATURE2_MAX
        {
            get
            {
                return _temp2Max;
            }

            set
            {
                if (value != _temp2Max)
                {
                    _temp2Max = value;
                    OnPropertyChanged("TEMPERATURE2_MAX");
                }
            }
        }

        public int RPM
        {
            get
            {
                return _rpm;
            }

            set
            {
                if (value != _rpm)
                {
                    _rpm = value;
                    OnPropertyChanged("RPM");
                }
            }
        }

        public int RPM_MAX
        {
            get
            {
                return _rpmMax;
            }

            set
            {
                if (value != _rpmMax)
                {
                    _rpmMax = value;
                    OnPropertyChanged("RPM_MAX");
                }
            }
        }

        public int FUEL
        {
            get
            {
                return _fuel;
            }

            set
            {
                if (value != _fuel)
                {
                    _fuel = value;
                    OnPropertyChanged("FUEL");
                }
            }
        }

        public int BARO_ALTITUDE
        {
            get
            {
                return _baroAltitude;
            }

            set
            {
                if (value != _baroAltitude)
                {
                    _baroAltitude = value;
                    OnPropertyChanged("BARO_ALTITUDE");
                }
            }
        }

        public int BARO_ALTITUDE_MIN
        {
            get
            {
                return _baroAltitudeMin;
            }

            set
            {
                if (value != _baroAltitudeMin)
                {
                    _baroAltitudeMin = value;
                    OnPropertyChanged("BARO_ALTITUDE_MIN");
                }
            }
        }

        public int BARO_ALTITUDE_MAX
        {
            get
            {
                return _baroAltitudeMax;
            }

            set
            {
                if (value != _baroAltitudeMax)
                {
                    _baroAltitudeMax = value;
                    OnPropertyChanged("BARO_ALTITUDE_MAX");
                }
            }
        }

        public int VARIO_SPEED
        {
            get
            {
                return _varioSpeed;
            }

            set
            {
                if (value != _varioSpeed)
                {
                    _varioSpeed = value;
                    OnPropertyChanged("VARIO_SPEED");
                }
            }
        }

        public int ACCLX
        {
            get
            {
                return _acclX;
            }

            set
            {
                if (value != _acclX)
                {
                    _acclX = value;
                    OnPropertyChanged("ACCLX");
                }
            }
        }

        public int ACCLY
        {
            get
            {
                return _acclY;
            }

            set
            {
                if (value != _acclY)
                {
                    _acclY = value;
                    OnPropertyChanged("ACCLY");
                }
            }
        }

        public int ACCLZ
        {
            get
            {
                return _acclZ;
            }

            set
            {
                if (value != _acclZ)
                {
                    _acclZ = value;
                    OnPropertyChanged("ACCLZ");
                }
            }
        }

        public int CURRENT
        {
            get
            {
                return _current;
            }

            set
            {
                if (value != _current)
                {
                    _current = value;
                    OnPropertyChanged("CURRENT");
                }
            }
        }

        public int CURRENT_MAX
        {
            get
            {
                return _currentMax;
            }

            set
            {
                if (value != _currentMax)
                {
                    _currentMax = value;
                    OnPropertyChanged("CURRENT_MAX");
                }
            }
        }

        public int VFAS
        {
            get
            {
                return _vfas;
            }

            set
            {
                if (value != _vfas)
                {
                    _vfas = value;
                    OnPropertyChanged("VFAS");
                }
            }
        }

        public int VFAS_MIN
        {
            get
            {
                return _vfasMin;
            }

            set
            {
                if (value != _vfasMin)
                {
                   _vfasMin = value;
                    OnPropertyChanged("VFAS_MIN");
                }
            }
        }

        public int AIRSPEED
        {
            get
            {
                return _airspeed;
            }

            set
            {
                if (value != _airspeed)
                {
                    _airspeed = value;
                    OnPropertyChanged("AIRSPEED");
                }
            }
        }

        public int AIRSPEED_MAX
        {
            get
            {
                return _airspeedMax;
            }

            set
            {
                if (value != _airspeedMax)
                {
                    _airspeedMax = value;
                    OnPropertyChanged("AIRSPEED_MAX");
                }
            }
        }

        public int GPS_SPEED_BP
        {
            get
            {
                return _gpsSpeedBP;
            }

            set
            {
                if (value != _gpsSpeedBP)
                {
                    _gpsSpeedBP = value;
                    OnPropertyChanged("GPS_SPEED_BP");
                }
            }
        }

        public int GPS_SPEED_MAX
        {
            get
            {
                return _gpsSpeedMax;
            }

            set
            {
                if (value != _gpsSpeedMax)
                {
                    _gpsSpeedMax = value;
                    OnPropertyChanged("GPS_SPEED_MAX");
                }
            }
        }

        // probably have to write an IValueConverter for this
        public UInt32 GPS_DATE_TIME
        {
            get
            {
                return _gpsDateTime;
            }

            set
            {
                if (value != _gpsDateTime)
                {
                    _gpsDateTime = value;
                    OnPropertyChanged("GPS_DATE_TIME");
                }
            }
        }

        public int GPS_YEAR
        {
            get
            {
                return _gpsYear;
            }

            set
            {
                if (value != _gpsYear)
                {
                    _gpsYear = value;
                    OnPropertyChanged("GPS_YEAR");
                }
            }
        }

        public int GPS_MONTH
        {
            get
            {
                return _gpsMonth;
            }

            set
            {
                if (value != _gpsMonth)
                {
                    _gpsMonth = value;
                    OnPropertyChanged("GPS_MONTH");
                }
            }
        }

        public int GPS_DAY
        {
            get
            {
                return _gpsDay;
            }

            set
            {
                if (value != _gpsDay)
                {
                    _gpsDay = value;
                    OnPropertyChanged("GPS_DAY");
                }
            }
        }

        public int GPS_HOUR
        {
            get
            {
                return _gpsHour;
            }

            set
            {
                if (value != _gpsHour)
                {
                    _gpsHour = value;
                    OnPropertyChanged("GPS_HOUR");
                }
            }
        }

        public int GPS_MINUTE
        {
            get
            {
                return _gpsMinute;
            }

            set
            {
                if (value != _gpsMinute)
                {
                    _gpsMinute = value;
                    OnPropertyChanged("GPS_MINUTE");
                }
            }
        }

        public int GPS_SECOND
        {
            get
            {
                return _gpsSecond;
            }

            set
            {
                if (value != _gpsSecond)
                {
                    _gpsSecond = value;
                    OnPropertyChanged("GPS_SECOND");
                }
            }
        }

        public int GPS_COURSE_BP
        {
            get
            {
                return _gpsCourseBp;
            }

            set
            {
                if (value != _gpsCourseBp)
                {
                    _gpsCourseBp = value;
                    OnPropertyChanged("GPS_COURSE_BP");
                }
            }
        }

        public int GPS_COURSE_AP
        {
            get
            {
                return _gpsCourseAp;
            }

            set
            {
                if (value != _gpsCourseAp)
                {
                    _gpsCourseAp = value;
                    OnPropertyChanged("GPS_COURSE_AP");
                }
            }
        }

        public int CELLS_COUNT
        {
            get
            {
                return _cellsCount;
            }

            set
            {
                if (value != _cellsCount)
                {
                    _cellsCount = value;
                    OnPropertyChanged("CELLS_COUNT");
                }
            }
        }

        public List<int> SENSOR_CELLS_COUNT_LIST
        {
            get
            {
                return _sensorCellsCountList;
            }

            set
            {
                if (value != _sensorCellsCountList)
                {
                    _sensorCellsCountList = value;
                    OnPropertyChanged("SENSOR_CELLS_COUNT_LIST");
                }
            }
        }

        public int CELLS_SUM
        {
            get
            {
                return _cellsSum;
            }

            set
            {
                if (value != _cellsSum)
                {
                    _cellsSum = value;
                    OnPropertyChanged("CELLS_SUM");
                }
            }
        }

        public int CELLS_SUM_MIN
        {
            get
            {
                return _cellsSumMin;
            }

            set
            {
                if (value != _cellsSumMin)
                {
                    _cellsSumMin = value;
                    OnPropertyChanged("CELLS_SUM_MIN");
                }
            }
        }

        public int MIN_CELL
        {
            get
            {
                return _minCell;
            }

            set
            {
                if (value != _minCell)
                {
                    _minCell = value;
                    OnPropertyChanged("MIN_CELL");
                }
            }
        }

        public int CELLS_STATE
        {
            get
            {
                return _cellsState;
            }

            set
            {
                if (value != _cellsState)
                {
                    _cellsState = value;
                    OnPropertyChanged("CELLS_STATE");
                }
            }
        }

        public int CELL_VOLTS_MIN
        {
            get
            {
                return _cellVoltsMin;
            }

            set
            {
                if (value != _cellVoltsMin)
                {
                    _cellVoltsMin = value;
                    OnPropertyChanged("CELL_VOLTS_MIN");
                }
            }
        }

        public int GPS_ALTITUDE
        {
            get
            {
                return _gpsAltitude;
            }

            set
            {
                if (value != _gpsAltitude)
                {
                    _gpsAltitude = value;
                    OnPropertyChanged("GPS_ALTITUDE");
                }
            }
        }

        public int GPS_ALTITUDE_BP
        {
            get
            {
                return _gpsAltitudeBP;
            }

            set
            {
                if (value != _gpsAltitudeBP)
                {
                    _gpsAltitudeBP = value;
                    OnPropertyChanged("GPS_ALTITUDE_BP");
                }
            }
        }

        public int GPS_ALTITUDE_OFFSET
        {
            get
            {
                return _gpsAltitudeOffset;
            }

            set
            {
                if (value != _gpsAltitudeOffset)
                {
                    _gpsAltitudeOffset = value;
                    OnPropertyChanged("GPS_ALTITUDE_OFFSET");
                }
            }
        }

        public int MIN_ALTITUDE
        {
            get
            {
                return _minAltitude;
            }

            set
            {
                if (value != _minAltitude)
                {
                    _minAltitude = value;
                    OnPropertyChanged("MIN_ALTITUDE");
                }
            }
        }

        public int MAX_ALTITUDE
        {
            get
            {
                return _maxAltitude;
            }

            set
            {
                if (value != _maxAltitude)
                {
                    _maxAltitude = value;
                    OnPropertyChanged("MAX_ALTITUDE");
                }
            }
        }

        public int PILOT_LATITUDE
        {
            get
            {
                return _pilotLatitude;
            }

            set
            {
                if (value != _pilotLatitude)
                {
                    _pilotLatitude = value;
                    OnPropertyChanged("PILOT_LATITUDE");
                }
            }
        }

        public int PILOT_LONGITUDE
        {
            get
            {
                return _pilotLongitude;
            }

            set
            {
                if (value != _pilotLongitude)
                {
                    _pilotLongitude = value;
                    OnPropertyChanged("PILOT_LONGITUDE");
                }
            }
        }

        public int DIST_FROM_EARTH_AXIS
        {
            get
            {
                return _distFromEarthAxis;
            }

            set
            {
                if (value != _distFromEarthAxis)
                {
                    _distFromEarthAxis = value;
                    OnPropertyChanged("DIST_FROM_EARTH_AXIS");
                }
            }
        }

        public int GPS_DISTANCE
        {
            get
            {
                return _gpsDistance;
            }

            set
            {
                if (value != _gpsDistance)
                {
                    _gpsDistance = value;
                    OnPropertyChanged("GPS_DISTANCE");
                }
            }
        }

        public int GPS_DISTANCE_MAX
        {
            get
            {
                return _gpsDistanceMax;
            }

            set
            {
                if (value != _gpsDistanceMax)
                {
                    _gpsDistanceMax = value;
                    OnPropertyChanged("GPS_DISTANCE_MAX");
                }
            }
        }

        public int GPS_LATITUDE_BP
        {
            get
            {
                return _gpsLatitudeBP;
            }

            set
            {
                if (value != _gpsLatitudeBP)
                {
                    _gpsLatitudeBP = value;
                    OnPropertyChanged("GPS_LATITUDE_BP");
                }
            }
        }

        public int GPS_LATITUDE_AP
        {
            get
            {
                return _gpsLatitudeAP;
            }

            set
            {
                if (value != _gpsLatitudeAP)
                {
                    _gpsLatitudeAP = value;
                    OnPropertyChanged("GPS_LATITUDE_AP");
                }
            }
        }

        public int GPS_LONGITUDE_BP
        {
            get
            {
                return _gpsLongitudeBP;
            }

            set
            {
                if (value != _gpsLongitudeBP)
                {
                    _gpsLongitudeBP = value;
                    OnPropertyChanged("GPS_LONGITUDE_BP");
                }
            }
        }

        public int GPS_LONGITUDE_AP
        {
            get
            {
                return _gpsLongitudeAP;
            }

            set
            {
                if (value != _gpsLongitudeAP)
                {
                    _gpsLongitudeAP = value;
                    OnPropertyChanged("GPS_LONGITUDE_AP");
                }
            }
        }

        public string GPS_LONGITUDE_EW
        {
            get
            {
                return _gpsLongitudeEW;
            }

            set
            {
                if (value != _gpsLongitudeEW)
                {
                    _gpsLongitudeEW = value;
                    OnPropertyChanged("GPS_LONGITUDE_EW");
                }
            }
        }

        public string GPS_LATITUDE_NS
        {
            get
            {
                return _gpsLatitudeNS;
            }

            set
            {
                if (value != _gpsLatitudeNS)
                {
                    _gpsLatitudeNS = value;
                    OnPropertyChanged("GPS_LATITUDE_NS");
                }
            }
        }

        public int GPS_FIX
        {
            get
            {
                return _gpsFix;
            }

            set
            {
                if (value != _gpsFix)
                {
                    _gpsFix = value;
                    OnPropertyChanged("GPS_FIX");
                }
            }
        }

        public int CELL1
        {
            get
            {
                return _cell1;
            }

            set
            {
                if (value != _cell1)
                {
                    _cell1 = value;
                    OnPropertyChanged("CELL1");
                }
            }
        }

        public int CELL2
        {
            get
            {
                return _cell2;
            }

            set
            {
                if (value != _cell2)
                {
                    _cell2 = value;
                    OnPropertyChanged("CELL2");
                }
            }
        }

        public int CELL3
        {
            get
            {
                return _cell3;
            }

            set
            {
                if (value != _cell3)
                {
                    _cell3 = value;
                    OnPropertyChanged("CELL3");
                }
            }
        }

        public int CELL4
        {
            get
            {
                return _cell4;
            }

            set
            {
                if (value != _cell4)
                {
                    _cell4 = value;
                    OnPropertyChanged("CELL4");
                }
            }
        }

        public int CELL5
        {
            get
            {
                return _cell5;
            }

            set
            {
                if (value != _cell5)
                {
                    _cell5 = value;
                    OnPropertyChanged("CELL5");
                }
            }
        }

        public int CELL6
        {
            get
            {
                return _cell6;
            }

            set
            {
                if (value != _cell6)
                {
                    _cell6 = value;
                    OnPropertyChanged("CELL6");
                }
            }
        }

        public int CELL7
        {
            get
            {
                return _cell7;
            }

            set
            {
                if (value != _cell7)
                {
                    _cell7 = value;
                    OnPropertyChanged("CELL7");
                }
            }
        }

        public int CELL8
        {
            get
            {
                return _cell8;
            }

            set
            {
                if (value != _cell8)
                {
                    _cell8 = value;
                    OnPropertyChanged("CELL8");
                }
            }
        }

        public int CELL9
        {
            get
            {
                return _cell9;
            }

            set
            {
                if (value != _cell9)
                {
                    _cell9 = value;
                    OnPropertyChanged("CELL9");
                }
            }
        }

        public int CELL10
        {
            get
            {
                return _cell10;
            }

            set
            {
                if (value != _cell10)
                {
                    _cell10 = value;
                    OnPropertyChanged("CELL10");
                }
            }
        }

        public int CELL11
        {
            get
            {
                return _cell11;
            }

            set
            {
                if (value != _cell11)
                {
                    _cell11 = value;
                    OnPropertyChanged("CELL11");
                }
            }
        }

        public int CELL12
        {
            get
            {
                return _cell12;
            }

            set
            {
                if (value != _cell12)
                {
                    _cell12 = value;
                    OnPropertyChanged("CELL12");
                }
            }
        }
    }
}
