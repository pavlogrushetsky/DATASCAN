using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Text;
using DATASCAN.Model.Floutecs;

namespace DATASCAN.Repositories.Extensions
{
    /// <summary>
    /// Статический класс с методами расширения для объектов данных вычислителей ФЛОУТЭК
    /// </summary>
    public static class FloutecDataExtensions
    {
        // Форматы дат для преобразования полей DAT и DAT_END
        private static readonly string[] datetimeFormats = { @"M/d/yyyy hh:mm:ss tt", @"dd.MM.yyyy HH:mm:ss", @"dd.MM.yyyy H:mm:ss", @"dd.MM.yyyy" };

        // Форматы дат для преобразования полей KONTRH
        private static readonly string[] timeFormats = { @"hh:mm:ss tt", @"HH:mm:ss", @"H:mm:ss" };

        #region Методы расширения для FloutecIdentData

        /// <summary>
        /// Метод расширения объекта данных идентификации для получения данных из таблицы данных идентификации
        /// </summary>
        /// <param name="identData">Объект данных идентификации</param>
        /// <param name="reader"><see cref="OleDbDataReader"/></param>
        public static void FromIdentTable(this FloutecIdentData identData, OleDbDataReader reader)
        {
            identData.KONTRH = DateTime.ParseExact(GetReaderValue(reader, "KONTRH", "").Trim(), timeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None).TimeOfDay;
            identData.SCHET = GetReaderValue(reader, "SCHET", 0);
            identData.KALIBSCH = GetReaderValue(reader, "KALIBSCH", 0.0);
            identData.MAXRSCH = GetReaderValue(reader, "MAXRSCH", 0.0);
            identData.MINRSCH = GetReaderValue(reader, "MINRSCH", 0.0);
            identData.VERXP = GetReaderValue(reader, "VERXP", 0.0);
            identData.VERXDP = GetReaderValue(reader, "VERXDP", 0.0);
            identData.KONDENS = GetReaderValue(reader, "KONDENS", 0);
            identData.NIZP = GetReaderValue(reader, "NIZP", 0.0);
            identData.VERXT = GetReaderValue(reader, "VERXT", 0.0);
            identData.NIZT = GetReaderValue(reader, "NIZT", 0.0);
            identData.TYPDAN = GetReaderValue(reader, "TYPDAN", 0.0);
        }

        /// <summary>
        /// Метод расширения объекта данных идентификации для получения данных из таблицы статических данных
        /// </summary>
        /// <param name="identData">Объект данных идентификации</param>
        /// <param name="reader"><see cref="OleDbDataReader"/></param>
        public static void FromStatTable(this FloutecIdentData identData, OleDbDataReader reader)
        {
            identData.PLOTN = GetReaderValue(reader, "PLOTN", 0.0);
            identData.CO2 = GetReaderValue(reader, "CO2", 0.0);
            identData.NO2 = GetReaderValue(reader, "NO2", 0.0);
            identData.DTRUB = GetReaderValue(reader, "DTRUB", 0.0);
            identData.DSU = GetReaderValue(reader, "DSU", 0.0);
            identData.OTSECH = GetReaderValue(reader, "OTSECH", 0.0);
            identData.OTBOR = GetReaderValue(reader, "OTBOR", 0);
            identData.ACP = GetReaderValue(reader, "ACP", 0.0);
            identData.BCP = GetReaderValue(reader, "BCP", 0.0);
            identData.CCP = GetReaderValue(reader, "CCP", 0.0);
            identData.ACS = GetReaderValue(reader, "ACS", 0.0);
            identData.BCS = GetReaderValue(reader, "BCS", 0.0);
            identData.CCS = GetReaderValue(reader, "CCS", 0.0);
        }

        /// <summary>
        /// Метод определения равенства объектов данных идентификации
        /// </summary>
        /// <param name="newData">Новые данные</param>
        /// <param name="exData">Существующие данные</param>
        /// <returns></returns>
        public static bool IsEqual(this FloutecIdentData newData, FloutecIdentData exData)
        {
            return newData != null && exData != null && newData.ACP.Equals(exData.ACP) && newData.ACS.Equals(exData.ACS) && newData.BCP.Equals(exData.BCP) && newData.BCS.Equals(exData.BCS)
                && newData.CCP.Equals(exData.CCP) && newData.CCS.Equals(exData.CCS) && newData.CO2.Equals(exData.CO2) && newData.DSU.Equals(exData.DSU) && newData.SCHET.Equals(exData.SCHET)
                && newData.DTRUB.Equals(exData.DTRUB) && newData.KALIBSCH.Equals(exData.KALIBSCH) && newData.KONDENS.Equals(exData.KONDENS) && newData.KONTRH.Equals(exData.KONTRH)
                && newData.MAXRSCH.Equals(exData.MAXRSCH) && newData.MINRSCH.Equals(exData.MINRSCH) && newData.NIZP.Equals(exData.NIZP) && newData.NIZT.Equals(exData.NIZT)
                && newData.NO2.Equals(exData.NO2) && newData.OTBOR.Equals(exData.OTBOR) && newData.OTSECH.Equals(exData.OTSECH) && newData.PLOTN.Equals(exData.PLOTN)
                && newData.SHER.Equals(exData.SHER) && newData.VERXDP.Equals(exData.VERXDP) && newData.VERXP.Equals(exData.VERXP) && newData.VERXT.Equals(exData.VERXT);
        }

        #endregion

        #region Методы расширения для FloutecHourlyData

        /// <summary>
        /// Метод расширения объекта часовых данных для получения данных из таблицы часовых данных
        /// </summary>
        /// <param name="hourlyData">Коллекция объектов часовых данных</param>
        /// <param name="reader"><see cref="OleDbDataReader"/></param>
        public static void FromHourTable(this List<FloutecHourlyData> hourlyData, OleDbDataReader reader)
        {
                FloutecHourlyData data = new FloutecHourlyData();

                data.DAT = DateTime.ParseExact(GetReaderValue(reader, "DAT", "").Trim(), datetimeFormats, new CultureInfo("en-US"), DateTimeStyles.None);
                data.DAT_END = DateTime.ParseExact(GetReaderValue(reader, "DAT_END", "").Trim(), datetimeFormats, new CultureInfo("en-US"), DateTimeStyles.None);
                data.RASX = GetReaderValue(reader, "RASX", 0.0);
                data.DAVL = GetReaderValue(reader, "DAVL", 0.0);
                data.PD = GetReaderValue(reader, "PD", "");
                data.TEMP = GetReaderValue(reader, "TEMP", 0.0);
                data.PT = GetReaderValue(reader, "PT", "");
                data.PEREP = GetReaderValue(reader, "PEREP", 0.0);
                data.PP = GetReaderValue(reader, "PP", "");
                data.PLOTN = GetReaderValue(reader, "PLOTN", 0.0);
                data.PL = GetReaderValue(reader, "PL", "");

                hourlyData.Add(data);
        }

        #endregion

        #region Методы расширения для FloutecInstantData

        /// <summary>
        /// Метод расширения объекта мгновенных данных для получения данных из таблицы мгновенных данных
        /// </summary>
        /// <param name="instantData">Объект мгновенных данных</param>
        /// <param name="reader"><see cref="OleDbDataReader"/></param>
        public static void FromInstTable(this FloutecInstantData instantData, OleDbDataReader reader)
        {
            instantData.DAT = DateTime.ParseExact(GetReaderValue(reader, "DAT", "").Trim(), datetimeFormats, new CultureInfo("en-US"), DateTimeStyles.None);
            instantData.ABSP = GetReaderValue(reader, "ABSP", 0.0);
            instantData.ALARMRY = GetReaderValue(reader, "ALARMRY", 0.0);
            instantData.ALARMSY = GetReaderValue(reader, "ALARMSY", 0.0);
            instantData.ALLSPEND = GetReaderValue(reader, "ALLSPEND", 0.0);
            instantData.CURRSPEND = GetReaderValue(reader, "CURRSPEND", 0.0);
            instantData.DAYSPEND = GetReaderValue(reader, "DAYSPEND", 0);
            instantData.DLITAS = GetReaderValue(reader, "DLITAS", 0);
            instantData.DLITBAS = GetReaderValue(reader, "DLITBAS", 0);
            instantData.DLITMAS = GetReaderValue(reader, "DLITMAS", 0);
            instantData.GASVIZ = GetReaderValue(reader, "GASVIZ", 0.0);
            instantData.GAZPLOTNNU = GetReaderValue(reader, "GAZPLOTNNU", 0.0);
            instantData.GAZPLOTNRU = GetReaderValue(reader, "GAZPLOTNRU", 0.0);
            instantData.LASTMONTHSPEND = GetReaderValue(reader, "LASTMONTHSPEND", 0.0);
            instantData.MONTHSPEND = GetReaderValue(reader, "MONTHSPEND", 0.0);
            instantData.PALARMRY = GetReaderValue(reader, "PALARMRY", 0.0);
            instantData.PALARMSY = GetReaderValue(reader, "PALARMSY", 0.0);
            instantData.PD = GetReaderValue(reader, "PD", "");
            instantData.PDLITAS = GetReaderValue(reader, "PDLITAS", 0);
            instantData.PDLITBAS = GetReaderValue(reader, "PDLITBAS", 0);
            instantData.PDLITMAS = GetReaderValue(reader, "PDLITMAS", 0);
            instantData.PERPRES = GetReaderValue(reader, "PERPRES", 0.0);
            instantData.PP = GetReaderValue(reader, "PP", "");
            instantData.PQHOUR = GetReaderValue(reader, "PQHOUR", 0.0);
            instantData.PT = GetReaderValue(reader, "PT", "");
            instantData.QHOUR = GetReaderValue(reader, "QHOUR", 0.0);
            instantData.SQROOT = GetReaderValue(reader, "SQROOT", 0.0);
            instantData.STPRES = GetReaderValue(reader, "STPRES", 0.0);
            instantData.TEMP = GetReaderValue(reader, "TEMP", 0.0);
            instantData.YESTSPEND = GetReaderValue(reader, "YESTSPEND", 0.0);
        }

        /// <summary>
        /// Метод определения равенства объектов мгновенных данных
        /// </summary>
        /// <param name="newData">Новые данные</param>
        /// <param name="exData">Существующие данные</param>
        /// <returns></returns>
        public static bool IsEqual(this FloutecInstantData newData, FloutecInstantData exData)
        {
            return newData != null && exData != null && newData.DAT.Equals(exData.DAT);
        }

        #endregion

        #region Методы расширения для FloutecAlarmData

        public static void FromAvarTable(this List<FloutecAlarmData> alarmData, OleDbDataReader reader)
        {
            FloutecAlarmData data = new FloutecAlarmData();

            data.DAT = DateTime.ParseExact(GetReaderValue(reader, "DAT", "").Trim(), datetimeFormats, new CultureInfo("en-US"), DateTimeStyles.None);
            data.T_AVAR = GetReaderValue(reader, "T_AVAR", 0);
            data.T_PARAM = GetReaderValue(reader, "T_PARAM", 0);
            data.VAL = GetReaderValue(reader, "VAL", 0.0);

            alarmData.Add(data);
        }

        #endregion

        #region Методы расширения для FloutecInterData

        public static void FromVmeshTable(this List<FloutecInterData> interData, OleDbDataReader reader)
        {
            FloutecInterData data = new FloutecInterData();

            data.DAT = DateTime.ParseExact(GetReaderValue(reader, "DAT", "").Trim(), datetimeFormats, new CultureInfo("en-US"), DateTimeStyles.None);
            data.CH_PAR = GetReaderValue(reader, "CH_PAR", 0);
            data.GetValues(reader);

            interData.Add(data);
        }

        private static void GetValues(this FloutecInterData data, OleDbDataReader reader)
        {
            byte bval11 = GetReaderValue(reader, "BVAL11", (byte)0);
            byte bval12 = GetReaderValue(reader, "BVAL12", (byte)0);
            byte bval13 = GetReaderValue(reader, "BVAL13", (byte)0);
            byte bval14 = GetReaderValue(reader, "BVAL14", (byte)0);

            byte bval21 = GetReaderValue(reader, "BVAL21", (byte)0);
            byte bval22 = GetReaderValue(reader, "BVAL22", (byte)0);
            byte bval23 = GetReaderValue(reader, "BVAL23", (byte)0);
            byte bval24 = GetReaderValue(reader, "BVAL24", (byte)0);

            if (data.CH_PAR == 0)
            {
                byte[] oldChars = { bval11, bval12, bval13, bval14 };
                byte[] newChars = { bval21, bval22, bval23, bval24 };

                data.VAL_OLD = Encoding.UTF8.GetString(oldChars);
                data.VAL_NEW = Encoding.UTF8.GetString(newChars);
            }
            else if ((data.CH_PAR >= 1 && data.CH_PAR <= 11) || 
                (data.CH_PAR >= 13 && data.CH_PAR <= 27) || 
                (data.CH_PAR >= 33 && data.CH_PAR <= 37) || 
                (data.CH_PAR >= 48 && data.CH_PAR <= 60) ||
                data.CH_PAR == 129 || data.CH_PAR == 132 || data.CH_PAR == 138 ||
                data.CH_PAR == 142 || data.CH_PAR == 143 ||
                (data.CH_PAR >= 147 && data.CH_PAR <= 150) || 
                (data.CH_PAR >= 153 && data.CH_PAR <= 177) ||
                (data.CH_PAR >= 181 && data.CH_PAR <= 185))
            {
                data.VAL_OLD = GetReaderValue(reader, "SVAL1", 0.0).ToString(CultureInfo.InvariantCulture);
                data.VAL_NEW = GetReaderValue(reader, "SVAL2", 0.0).ToString(CultureInfo.InvariantCulture);
            }
            else if (data.CH_PAR == 12)
            {
                if (bval11 == 0)
                    data.VAL_OLD = "Угловой";
                else if (bval11 == 1)
                    data.VAL_OLD = "Фланцевый";
                else
                    data.VAL_OLD = "";

                if (bval21 == 0)
                    data.VAL_NEW = "Угловой";
                else if (bval21 == 1)
                    data.VAL_NEW = "Фланцевый";
                else
                    data.VAL_NEW = "";
            }
            else if (data.CH_PAR == 28 || data.CH_PAR == 29)
            {
                bool oldDayFormat = (bval12 & (1 << 7)) != 0;
                bool newDayFormat = (bval22 & (1 << 7)) != 0;

                if (bval11 > 0)
                {
                    if (!oldDayFormat)
                    {
                        if (bval13 == 1 || bval13 == 21)
                            data.VAL_OLD = bval13 + " час ";
                        else if ((bval13 >= 2 && bval13 <= 4) || (bval13 >= 22 && bval13 <= 24))
                            data.VAL_OLD = bval13 + " часа ";
                        else
                            data.VAL_OLD = bval13 + " часов ";

                        data.VAL_OLD = data.VAL_OLD + (bval12 >> 4) + "-го " + GetMonthName(bval11);
                    }
                    else
                    {
                        bool dayCountDirection = (bval12 & (1 << 5)) != 0;
                        int dayOfWeek = (bval12 >> 3) & 7;
                        int weekOfMonth = (bval12 >> 0) & 7;

                        if (!dayCountDirection)
                            data.VAL_OLD = GetDayName(weekOfMonth, dayOfWeek);
                        else
                            data.VAL_OLD = GetOrdinalDayName(weekOfMonth, dayOfWeek);

                        data.VAL_OLD = data.VAL_OLD + " " + GetMonthName(bval11) + ", ";

                        if (bval13 == 1 || bval13 == 21)
                            data.VAL_OLD = data.VAL_OLD + bval13 + " час";
                        else if ((bval13 >= 2 && bval13 <= 4) || (bval13 >= 22 && bval13 <= 24))
                            data.VAL_OLD = data.VAL_OLD + bval13 + " часа";
                        else
                            data.VAL_OLD = data.VAL_OLD + bval13 + " часов";
                    }
                }
                else
                    data.VAL_OLD = "Запрет перехода";

                if (bval21 > 0)
                {
                    if (!newDayFormat)
                    {
                        if (bval23 == 1 || bval23 == 21)
                            data.VAL_NEW = bval23 + " час ";
                        else if ((bval23 >= 2 && bval23 <= 4) || (bval23 >= 22 && bval23 <= 24))
                            data.VAL_NEW = bval23 + " часа ";
                        else
                            data.VAL_NEW = bval23 + " часов ";

                        data.VAL_NEW = data.VAL_NEW + (bval22 >> 4) + "-го " + GetMonthName(bval21);
                    }
                    else
                    {
                        bool dayCountDirection = (bval22 & (1 << 6)) != 0;
                        int dayOfWeek = (bval22 >> 3) & 7;
                        int weekOfMonth = (bval22 >> 0) & 7;

                        if (!dayCountDirection)
                            data.VAL_NEW = GetDayName(weekOfMonth, dayOfWeek);
                        else
                            data.VAL_NEW = GetOrdinalDayName(weekOfMonth, dayOfWeek);

                        data.VAL_NEW = data.VAL_NEW + " " + GetMonthName(bval21) + ", ";

                        if (bval23 == 1 || bval23 == 21)
                            data.VAL_NEW = data.VAL_NEW + bval23 + " час";
                        else if ((bval23 >= 2 && bval23 <= 4) || (bval23 >= 22 && bval23 <= 24))
                            data.VAL_NEW = data.VAL_NEW + bval23 + " часа";
                        else
                            data.VAL_NEW = data.VAL_NEW + bval23 + " часов";
                    }
                }
                else
                    data.VAL_NEW = "Запрет перехода";

            }
            else if ((data.CH_PAR >= 30 && data.CH_PAR <= 32) || data.CH_PAR == 46 || data.CH_PAR == 47
                || data.CH_PAR == 130 || data.CH_PAR == 131 || 
                (data.CH_PAR >= 186 && data.CH_PAR <= 188) || data.CH_PAR == 191 || data.CH_PAR == 192)
            {
                data.VAL_OLD = bval11.ToString();
                data.VAL_NEW = bval21.ToString();
            }
            else if (data.CH_PAR >= 38 && data.CH_PAR <= 40) // Модели ?????
            {
                data.VAL_OLD = "";
                data.VAL_NEW = "";
            }
            else if (data.CH_PAR == 41)
            {
                if (bval11 == 0)
                    data.VAL_OLD = "кг/см2";
                else if (bval11 == 1)
                    data.VAL_OLD = "МПа";
                else
                    data.VAL_OLD = "";

                if (bval21 == 0)
                    data.VAL_NEW = "кг/см2";
                else if (bval21 == 1)
                    data.VAL_NEW = "МПа";
                else
                    data.VAL_NEW = "";
            }
            else if (data.CH_PAR == 42)
            {
                if (bval11 == 0)
                    data.VAL_OLD = "кг/м2";
                else if (bval11 == 1)
                    data.VAL_OLD = "кПа";
                else
                    data.VAL_OLD = "";

                if (bval21 == 0)
                    data.VAL_NEW = "кг/м2";
                else if (bval21 == 1)
                    data.VAL_NEW = "кПа";
                else
                    data.VAL_NEW = "";
            }
            else if (data.CH_PAR == 43) // Ед. измерения атм. давления ?????
            {
                data.VAL_OLD = "";
                data.VAL_NEW = "";
            }
            else if (data.CH_PAR == 44)
            {
                if (bval11 == 0)
                    data.VAL_OLD = "Абсолютное";
                else if (bval11 == 1)
                    data.VAL_OLD = "Избыточное";
                else
                    data.VAL_OLD = "";

                if (bval21 == 0)
                    data.VAL_NEW = "Абсолютное";
                else if (bval21 == 1)
                    data.VAL_NEW = "Избыточное";
                else
                    data.VAL_NEW = "";
            }
            else if (data.CH_PAR == 45)
            {
                if (bval11 == 0)
                    data.VAL_OLD = "Ненужный";
                else if (bval11 == 1)
                    data.VAL_OLD = "Необходимый";
                else
                    data.VAL_OLD = "";

                if (bval21 == 0)
                    data.VAL_NEW = "Ненужный";
                else if (bval21 == 1)
                    data.VAL_NEW = "Необходимый";
                else
                    data.VAL_NEW = "";
            }
            else if (data.CH_PAR >= 61 && data.CH_PAR <= 72)
            {
                if (bval11 == 0)
                    data.VAL_OLD = "Выключено";
                else if (bval11 == 1)
                    data.VAL_OLD = "Включено";
                else
                    data.VAL_OLD = "";

                if (bval21 == 0)
                    data.VAL_NEW = "Выключено";
                else if (bval21 == 1)
                    data.VAL_NEW = "Включено";
                else
                    data.VAL_NEW = "";
            }
            else if (data.CH_PAR >= 61 && data.CH_PAR <= 72)
            {
                if (bval11 == 0)
                    data.VAL_OLD = "Перепад";
                else if (bval11 == 1)
                    data.VAL_OLD = "Счётчик";
                else
                    data.VAL_OLD = "";

                if (bval21 == 0)
                    data.VAL_NEW = "Перепад";
                else if (bval21 == 1)
                    data.VAL_NEW = "Счётчик";
                else
                    data.VAL_NEW = "";
            }
            else if(data.CH_PAR == 128)
            {
                DateTime oldTime = new DateTime(1, 1, 1, bval11, bval12, bval13);
                DateTime newTime = new DateTime(1, 1, 1, bval21, bval22, bval23);

                data.VAL_OLD = oldTime.ToString("HH:mm:ss");
                data.VAL_NEW = newTime.ToString("HH:mm:ss");
            }
            else if (data.CH_PAR == 144 || data.CH_PAR == 179)
            {
                data.T_PARAM = bval11;
                data.VAL_OLD = "Измерение";
                data.VAL_NEW = GetReaderValue(reader, "SVAL2", 0.0).ToString(CultureInfo.InvariantCulture);
            }
            else if (data.CH_PAR == 145 || data.CH_PAR == 180)
            {
                data.T_PARAM = bval21;
                data.VAL_NEW = "Измерение";
                data.VAL_OLD = GetReaderValue(reader, "SVAL1", 0.0).ToString(CultureInfo.InvariantCulture);
            }
            else if (data.CH_PAR == 146 || data.CH_PAR == 151)
            {
                data.T_PARAM = bval11;
                data.VAL_NEW = "";
                data.VAL_OLD = "";
            }
            else if (data.CH_PAR == 178)
            {
                data.T_PARAM = bval11;
                data.VAL_NEW = "";
                data.VAL_OLD = "HART команда " + bval21;
            }
            else if (data.CH_PAR == 189)
            {
                if (bval11 == 0)
                    data.VAL_OLD = "LE,le";
                else if (bval11 == 1)
                    data.VAL_OLD = "LE,be";
                else if (bval11 == 2)
                    data.VAL_OLD = "BE,le";
                else if (bval11 == 3)
                    data.VAL_OLD = "BE,be";
                else
                    data.VAL_OLD = "";

                if (bval21 == 0)
                    data.VAL_NEW = "LE,le";
                else if (bval21 == 1)
                    data.VAL_NEW = "LE,be";
                else if (bval21 == 2)
                    data.VAL_NEW = "BE,le";
                else if (bval21 == 3)
                    data.VAL_NEW = "BE,be";
                else
                    data.VAL_NEW = "";
            }
            else if (data.CH_PAR == 190)
            {
                if (bval11 == 0)
                    data.VAL_OLD = "ASCII";
                else if (bval11 == 1)
                    data.VAL_OLD = "RTU";
                else
                    data.VAL_OLD = "";

                if (bval21 == 0)
                    data.VAL_NEW = "ASCII";
                else if (bval21 == 1)
                    data.VAL_NEW = "RTU";
                else
                    data.VAL_NEW = "";
            }
            else
            {
                data.VAL_OLD = "";
                data.VAL_NEW = "";
            }
        }

        #endregion

        #region Вспомогательные методы

        // Метод для получения записи с преобразованием к необходимому формату данных 
        private static T GetReaderValue<T>(OleDbDataReader reader, string ordinal, T defaultValue = default(T))
        {
            try
            {
                return (T)Convert.ChangeType(reader[ordinal], typeof(T));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        private static string GetMonthName(int monthNumber)
        {
            switch (monthNumber)
            {
                case 1:
                    return "января";
                case 2:
                    return "февраля";
                case 3:
                    return "марта";
                case 4:
                    return "апреля";
                case 5:
                    return "мая";
                case 6:
                    return "июня";
                case 7:
                    return "июля";
                case 8:
                    return "августа";
                case 9:
                    return "сентября";
                case 10:
                    return "октября";
                case 11:
                    return "ноября";
                case 12:
                    return "декабря";
                default:
                    return "";
            }
        }

        private static string GetOrdinalDayName(int weekOfMonth, int dayOfWeek)
        {
            string ret;

            if (weekOfMonth == 1)
                ret = "Последн";
            else if (weekOfMonth == 2)
                ret = "Предпоследн";
            else if (weekOfMonth == 3)
                ret = "Предпредпоследн";
            else
                return "";

            if (dayOfWeek == 1)
                ret = ret + "ее воскресенье";
            else if (dayOfWeek == 2)
                ret = ret + "ий понедельник";
            else if (dayOfWeek == 3)
                ret = ret + "ий вторник";
            else if (dayOfWeek == 4)
                ret = ret + "яя среда";
            else if (dayOfWeek == 5)
                ret = ret + "ий четверг";
            else if (dayOfWeek == 6)
                ret = ret + "яя пятница";
            else if (dayOfWeek == 7)
                ret = ret + "яя суббота";
            else
                return "";

            return ret;
        }

        private static string GetDayName(int weekOfMonth, int dayOfWeek)
        {
            if (dayOfWeek == 1)
                return weekOfMonth + "-е воскресенье";
            if (dayOfWeek == 2)
                return weekOfMonth + "-й понедельник";
            if (dayOfWeek == 3)
                return weekOfMonth + "-й вторник";
            if (dayOfWeek == 4)
                return weekOfMonth + "-я среда";
            if (dayOfWeek == 5)
                return weekOfMonth + "-й четверг";
            if (dayOfWeek == 6)
                return weekOfMonth + "-я пятница";
            if (dayOfWeek == 7)
                return weekOfMonth + "-я суббота";
            return "";
        }

        #endregion
    }
}
