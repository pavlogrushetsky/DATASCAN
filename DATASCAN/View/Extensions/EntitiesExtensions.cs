using System.Linq;
using DATASCAN.Model;
using DATASCAN.Model.Floutecs;
using DATASCAN.Model.Rocs;
using DATASCAN.Model.Scanning;

namespace DATASCAN.View.Extensions
{
    public static class EntitiesExtensions
    {
        /// <summary>
        /// Возвращает полную информацию о заказчике
        /// </summary>
        public static string Info(this Customer customer)
        {
            return $"Id:\t\t\t{customer.Id}\n" +
                   $"Назва:\t\t\t{customer.Title}\n" +
                   $"Контактна особа:\t{customer.Person}\n" +
                   $"Номер телефону:\t{customer.Phone}\n" +
                   $"Електронна пошта:\t{customer.Email}\n" +
                   $"Створено:\t\t{customer.DateCreated.ToString("dd.MM.yyyy HH:mm")}\n" +
                   $"Змінено:\t\t{customer.DateModified.ToString("dd.MM.yyyy HH:mm")}";
        }

        /// <summary>
        /// Возвращает полную информацию о группе вычислителей
        /// </summary>
        public static string Info(this EstimatorsGroup group)
        {
            return $"Id:\t\t{group.Id}\n" +
                   $"Назва:\t\t{group.Name}\n" +
                   $"Створено:\t{group.DateCreated.ToString("dd.MM.yyyy HH:mm")}\n" +
                   $"Змінено:\t{group.DateModified.ToString("dd.MM.yyyy HH:mm")}";
        }

        /// <summary>
        /// Возвращает полную информацию о вычислителе ФЛОУТЭК
        /// </summary>
        public static string Info(this Floutec floutec)
        {
            return $"Id:\t\t\t{floutec.Id}\n" +
                   $"Назва:\t\t\t{floutec.Name}\n" +
                   $"Опис:\t\t\t{floutec.Description}\n" +
                   $"Адреса:\t\t{floutec.Address}\n" +
                   $"Номер телефону:\t{floutec.Phone}\n" +
                   "Опитування:\t\t" + (floutec.IsScannedViaGPRS ? "По GPRS" : "Таблиці DBF") + "\n" +
                   $"Створено:\t\t{floutec.DateCreated.ToString("dd.MM.yyyy HH:mm")}\n" +
                   $"Змінено:\t\t{floutec.DateModified.ToString("dd.MM.yyyy HH:mm")}";
        }

        /// <summary>
        /// Возвращает полную информацию о вычислителе ROC809
        /// </summary>
        public static string Info(this Roc809 roc)
        {
            return $"Id:\t\t\t{roc.Id}\n" +
                   $"Назва:\t\t\t{roc.Name}\n" +
                   $"Опис:\t\t\t{roc.Description}\n" +
                   $"ROC Unit:\t\t{roc.RocUnit}\n" +
                   $"ROC Group:\t\t{roc.RocGroup}\n" +
                   $"Host Unit:\t\t{roc.HostUnit}\n" +
                   $"Host Group:\t\t{roc.HostGroup}\n" +
                   $"IP-адреса:\t\t{roc.Address}\n" +
                   $"Порт:\t\t\t{roc.Port}\n" +
                   $"Номер телефону:\t{roc.Phone}\n" +
                   "Опитування:\t\t" + (roc.IsScannedViaGPRS ? "По GPRS" : "По TCP/IP") + "\n" +
                   $"Створено:\t\t{roc.DateCreated.ToString("dd.MM.yyyy HH:mm")}\n" +
                   $"Змінено:\t\t{roc.DateModified.ToString("dd.MM.yyyy HH:mm")}";
        }

        /// <summary>
        /// Возвращает текст для элемента дерева для вычислителя
        /// </summary>
        public static string NodeTitle(this EstimatorBase estimator)
        {
            Floutec floutec = estimator as Floutec;
            if (floutec != null) return $"{floutec.Name} (ФЛОУТЕК, Адреса = {floutec.Address})";

            Roc809 roc = estimator as Roc809;
            if (roc != null) return roc.IsScannedViaGPRS ? 
                    $"{roc.Name} (ROC, Телефон = {roc.Phone})" : 
                    $"{roc.Name} (ROC, Адреса = {roc.Address})";

            return string.Empty;
        }

        /// <summary>
        /// Возвращает текст для элемента дерева для точки измерения
        /// </summary>
        public static string NodeTitle(this MeasurePointBase point)
        {            
            FloutecMeasureLine floutecLine = point as FloutecMeasureLine;
            if (floutecLine != null)
            {
                string sensor;

                switch (floutecLine.SensorType)
                {
                    case 0:
                        sensor = "Діафрагма";
                        break;
                    case 1:
                        sensor = "Лічильник";
                        break;
                    case 2:
                        sensor = "Витратомір";
                        break;
                    default:
                        sensor = "Діафрагма";
                        break;
                }

                return $"{floutecLine.Number} {floutecLine.Name} ({sensor})";
            }

            Roc809MeasurePoint rocPoint = point as Roc809MeasurePoint;
            if (rocPoint != null) return $"{rocPoint.Number} {rocPoint.Name} (Сегмент = {rocPoint.HistSegment})";

            return string.Empty;
        }

        /// <summary>
        /// Возвращает текст для элемента дерева опросов
        /// </summary>
        public static string NodeTitle(this ScanBase scan)
        {
            PeriodicScan periodic = scan as PeriodicScan;
            if (periodic != null)
            {
                string type = periodic.PeriodType ? "год." : "хв.";
                return $"{periodic.Title} ({periodic.Period} {type})";
            }

            ScheduledScan scheduled = scan as ScheduledScan;
            if (scheduled != null)
            {
                return $"{scheduled.Title} ({scheduled.Periods.FirstOrDefault()?.Period.ToString(@"hh\:mm")})";
            } 

            return string.Empty;
        }

        /// <summary>
        /// Возвращает полную информацию о измерительной нитке вычислителя ФЛОУТЭК
        /// </summary>
        public static string Info(this FloutecMeasureLine line)
        {
            string sensor;

            switch (line.SensorType)
            {
                case 0:
                    sensor = "Діафрагма";
                    break;
                case 1:
                    sensor = "Лічильник";
                    break;
                case 2:
                    sensor = "Витратомір";
                    break;
                default:
                    sensor = "Діафрагма";
                    break;
            }

            return $"Id:\t\t{line.Id}\n" +
                   $"Назва:\t\t{line.Name}\n" +
                   $"Опис:\t\t{line.Description}\n" +
                   $"Номер:\t\t{line.Number}\n" +
                   $"Тип сенсора:\t{sensor}\n" +
                   $"Створено:\t{line.DateCreated.ToString("dd.MM.yyyy HH:mm")}\n" +
                   $"Змінено:\t{line.DateModified.ToString("dd.MM.yyyy HH:mm")}";
        }

        /// <summary>
        /// Возвращает полную информацию о измерительной точке вычислителя ROC809
        /// </summary>
        public static string Info(this Roc809MeasurePoint point)
        {
            return $"Id:\t\t\t{point.Id}\n" +
                   $"Назва:\t\t\t{point.Name}\n" +
                   $"Опис:\t\t\t{point.Description}\n" +
                   $"Номер:\t\t\t{point.Number}\n" +
                   $"Історичний сегмент:\t{point.HistSegment}\n" +
                   $"Створено:\t\t{point.DateCreated.ToString("dd.MM.yyyy HH:mm")}\n" +
                   $"Змінено:\t\t{point.DateModified.ToString("dd.MM.yyyy HH:mm")}";
        }
    }
}