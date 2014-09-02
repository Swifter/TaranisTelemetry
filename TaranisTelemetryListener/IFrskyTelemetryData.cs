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

namespace TaranisTelemetryListener
{
    // these are all the relevant properties
    public interface IFrskyTelemetryData
    {
        int RSSI
        {
            get;
            set;
        }

        int RSSI_MIN
        {
            get;
            set;
        }

        int SWR
        {
            get;
            set;
        }

        int SWR_MIN
        {
            get;
            set;
        }

        int ADC1
        {
            get;
            set;
        }

        int ADC1_MIN
        {
            get;
            set;
        }

        int ADC1_MAX
        {
            get;
            set;
        }

        int ADC2
        {
            get;
            set;
        }

        int ADC2_MIN
        {
            get;
            set;
        }

        int ADC2_MAX
        {
            get;
            set;
        }

        int ADC3
        {
            get;
            set;
        }

        int ADC3_MIN
        {
            get;
            set;
        }

        int ADC3_MAX
        {
            get;
            set;
        }

        int ADC4
        {
            get;
            set;
        }

        int ADC4_MIN
        {
            get;
            set;
        }

        int ADC4_MAX
        {
            get;
            set;
        }

        int BATT_ID
        {
            get;
            set;
        }

        int TEMPERATURE1
        {
            get;
            set;
        }

        int TEMPERATURE1_MAX
        {
            get;
            set;
        }

        int TEMPERATURE2
        {
            get;
            set;
        }

        int TEMPERATURE2_MAX
        {
            get;
            set;
        }

        int RPM
        {
            get;
            set;
        }

        int RPM_MAX
        {
            get;
            set;
        }

        int FUEL
        {
            get;
            set;
        }

        int BARO_ALTITUDE
        {
            get;
            set;
        }

        int BARO_ALTITUDE_MIN
        {
            get;
            set;
        }

        int BARO_ALTITUDE_MAX
        {
            get;
            set;
        }

        int VARIO_SPEED
        {
            get;
            set;
        }

        int ACCLX
        {
            get;
            set;
        }

        int ACCLY
        {
            get;
            set;
        }

        int ACCLZ
        {
            get;
            set;
        }

        int CURRENT
        {
            get;
            set;
        }

        int CURRENT_MAX
        {
            get;
            set;
        }

        int VFAS
        {
            get;
            set;
        }

        int VFAS_MIN
        {
            get;
            set;
        }

        int AIRSPEED
        {
            get;
            set;
        }

        int AIRSPEED_MAX
        {
            get;
            set;
        }

        int GPS_SPEED_BP
        {
            get;
            set;
        }

        int GPS_SPEED_MAX
        {
            get;
            set;
        }

        // probably have to write an IValueConverter for this
        UInt32 GPS_DATE_TIME
        {
            get;
            set;
        }

        int GPS_YEAR
        {
            get;
            set;
        }

        int GPS_MONTH
        {
            get;
            set;
        }

        int GPS_DAY
        {
            get;
            set;
        }

        int GPS_HOUR
        {
            get;
            set;
        }

        int GPS_MINUTE
        {
            get;
            set;
        }

        int GPS_SECOND
        {
            get;
            set;
        }

        int GPS_COURSE_BP
        {
            get;
            set;
        }

        int GPS_COURSE_AP
        {
            get;
            set;
        }

        int CELLS_COUNT
        {
            get;
            set;
        }

        List<int> SENSOR_CELLS_COUNT_LIST
        {
            get;
            set;
        }

        int CELLS_SUM
        {
            get;
            set;
        }

        int CELLS_SUM_MIN
        {
            get;
            set;
        }

        int MIN_CELL
        {
            get;
            set;
        }

        int CELLS_STATE
        {
            get;
            set;
        }

        int CELL_VOLTS_MIN
        {
            get;
            set;
        }

        int GPS_ALTITUDE
        {
            get;
            set;
        }

        int GPS_ALTITUDE_BP
        {
            get;
            set;
        }

        int GPS_ALTITUDE_OFFSET
        {
            get;
            set;
        }

        int MIN_ALTITUDE
        {
            get;
            set;
        }

        int MAX_ALTITUDE
        {
            get;
            set;
        }

        int PILOT_LATITUDE
        {
            get;
            set;
        }

        int PILOT_LONGITUDE
        {
            get;
            set;
        }

        int DIST_FROM_EARTH_AXIS
        {
            get;
            set;
        }

        int GPS_DISTANCE
        {
            get;
            set;
        }

        int GPS_DISTANCE_MAX
        {
            get;
            set;
        }

        int GPS_LATITUDE_BP
        {
            get;
            set;
        }

        int GPS_LATITUDE_AP
        {
            get;
            set;
        }

        int GPS_LONGITUDE_BP
        {
            get;
            set;
        }

        int GPS_LONGITUDE_AP
        {
            get;
            set;
        }

        string GPS_LONGITUDE_EW
        {
            get;
            set;
        }

        string GPS_LATITUDE_NS
        {
            get;
            set;
        }

        int GPS_FIX
        {
            get;
            set;
        }

        int CELL1
        {
            get;
            set;
        }

        int CELL2
        {
            get;
            set;
        }

        int CELL3
        {
            get;
            set;
        }

        int CELL4
        {
            get;
            set;
        }

        int CELL5
        {
            get;
            set;
        }

        int CELL6
        {
            get;
            set;
        }

        int CELL7
        {
            get;
            set;
        }

        int CELL8
        {
            get;
            set;
        }

        int CELL9
        {
            get;
            set;
        }

        int CELL10
        {
            get;
            set;
        }

        int CELL11
        {
            get;
            set;
        }

        int CELL12
        {
            get;
            set;
        }
    }
}
