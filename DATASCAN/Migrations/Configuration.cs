﻿using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DATASCAN.Context;
using DATASCAN.Model.Floutecs.Catalogs;
using DATASCAN.Model.Rocs.Catalogs;

namespace DATASCAN.Migrations
{
    /// <summary>
    /// Конфигурация миграций
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            // Включение автоматических миграций
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// Инициализация справочных данных контекста
        /// </summary>
        /// <param name="context">Контекст данных</param>
        protected override void Seed(DataContext context)
        {
            List<FloutecParamTypes> floutecParamTypes = new List<FloutecParamTypes>
            {
                new FloutecParamTypes { Code = 0, Param = "Д", Description = "Давление"},
                new FloutecParamTypes { Code = 1, Param = "Т", Description = "Температура" },
                new FloutecParamTypes { Code = 2, Param = "ПД", Description = "Перепад давления" },
                new FloutecParamTypes { Code = 3, Param = "ПДН", Description = "Перепад давления низкий" },
                new FloutecParamTypes { Code = 4, Param = "ПДВ", Description = "Перепад давления высокий" },
                new FloutecParamTypes { Code = 5, Param = "Пимп", Description = "Период импульсов от плотномера" },
                new FloutecParamTypes { Code = 7, Param = "П", Description = "Плотность" },
                new FloutecParamTypes { Code = 8, Param = "Пимп,Т", Description = "Период импульсов от плотномера, температура среды в плотномере" },
                new FloutecParamTypes { Code = 11, Param = "ПД,Д", Description = "Перепад давления, давление" },
                new FloutecParamTypes { Code = 15, Param = "ПД,Д,Т", Description = "Перепад давления, давление, температура" },
                new FloutecParamTypes { Code = 39, Param = "Р", Description = "Расход" },
                new FloutecParamTypes { Code = 40, Param = "С", Description = "Счётчик" },
                new FloutecParamTypes { Code = 42, Param = "Р,Т,П", Description = "Расход, температура, плотность" }
            };

            if (!context.FloutecParamTypes.Any())
            {
                floutecParamTypes.ForEach(p => { context.FloutecParamTypes.AddOrUpdate(p); });
            }            

            List<FloutecAlarmTypes> floutecAlarmTypes = new List<FloutecAlarmTypes>
            {
                new FloutecAlarmTypes { Code = 0, Description = "Опрос в норме, конец замены предыдущим значением", Description_45 = "Опрос в норме, конец замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 128, Description = "Опрос не в норме, начало замены предыдущим значением", Description_45 = "Опрос не в норме, начало замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 1, Description = "Конец обслуживания канала", Description_45 = "Конец формирования НСХП канала" },
                new FloutecAlarmTypes { Code = 129, Description = "Начало обслуживания канала", Description_45 = "Начало формирования НСХП канала" },
                new FloutecAlarmTypes { Code = 2, Description = "Перепад давления выше значения отсечки", Description_45 = "Перепад давления выше значения отсечки" },
                new FloutecAlarmTypes { Code = 130, Description = "Перепад давления ниже или равен отсечке", Description_45 = "Перепад давления ниже или равен отсечке" },
                new FloutecAlarmTypes { Code = 3, Description = "Конец замены измерения константой", Description_45 = "Конец замены измерения константой" },
                new FloutecAlarmTypes { Code = 4, Description = "Конец замены измерения несанкционированной константой", Description_45 = "Конец замены измерения несанкционированной константой" },
                new FloutecAlarmTypes { Code = 132, Description = "Начало замены измерения несанкционированной константой", Description_45 = "Начало замены измерения несанкционированной константой" },
                new FloutecAlarmTypes { Code = 131, Description = "Начало замены измерения константой", Description_45 = "Начало замены измерения константой" },
                new FloutecAlarmTypes { Code = 5, Description = "Напряжение питания стало нормальным", Description_45 = "Напряжение питания стало нормальным" },
                new FloutecAlarmTypes { Code = 133, Description = "Напряжение питания ниже допуска", Description_45 = "Напряжение питания ниже допуска" },
                new FloutecAlarmTypes { Code = 6, Description = "Перепад давления стал ниже верхнего предела измерений", Description_45 = "Перепад давления стал ниже верхнего предела измерений" },
                new FloutecAlarmTypes { Code = 134, Description = "Перепад давления стал выше верхнего предела измерений", Description_45 = "Перепад давления стал выше верхнего предела измерений" },
                new FloutecAlarmTypes { Code = 7, Description = "Первое включение вычислителя после конфигурирования, подано напряжение питания вычислителя", Description_45 = "Первое включение вычислителя после конфигурирования, подано напряжение питания вычислителя" },
                new FloutecAlarmTypes { Code = 135, Description = "Снято напряжение питания вычислителя", Description_45 = "Снято напряжение питания вычислителя" },
                new FloutecAlarmTypes { Code = 138, Description = "Данные часов/календаря недостоверны", Description_45 = "Данные часов/календаря недостоверны" },
                new FloutecAlarmTypes { Code = 11, Description = "Отношение ПД/Д стало нормальным", Description_45 = "Отношение ПД/Д стало нормальным" },
                new FloutecAlarmTypes { Code = 139, Description = "Отношение ПД/Д не в норме", Description_45 = "Отношение ПД/Д не в норме" },
                new FloutecAlarmTypes { Code = 12, Description = "Re стало нормальным", Description_45 = "Re стало нормальным" },
                new FloutecAlarmTypes { Code = 140, Description = "Re вышло за допускаемые пределы", Description_45 = "Re вышло за допускаемые пределы" },
                new FloutecAlarmTypes { Code = 13, Description = "Конец деления на 0, конец замены предыдущим значением", Description_45 = "Конец деления на 0, конец замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 141, Description = "Начало деления на 0, начало замены предыдущим значением", Description_45 = "Начало деления на 0, начало замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 14, Description = "Конец работы от аккумулятора", Description_45 = "Конец работы от аккумулятора" },
                new FloutecAlarmTypes { Code = 142, Description = "Начало работы от аккумулятора", Description_45 = "Начало работы от аккумулятора" },
                new FloutecAlarmTypes { Code = 15, Description = "Температура стала выше -40 °С", Description_45 = "Температура стала выше -40 °С" },
                new FloutecAlarmTypes { Code = 143, Description = "Температура стала ниже -40 °С", Description_45 = "Температура стала ниже -40 °С" },
                new FloutecAlarmTypes { Code = 16, Description = "Температура стала ниже 80 °С", Description_45 = "Температура стала ниже 80 °С" },
                new FloutecAlarmTypes { Code = 144, Description = "Температура стала выше 80 °С", Description_45 = "Температура стала выше 80 °С" },
                new FloutecAlarmTypes { Code = 145, Description = "Время вычислителя и ПК отличаются более чем на 10 минут", Description_45 = "Время вычислителя и ПК отличаются более чем на 10 минут" },
                new FloutecAlarmTypes { Code = 18, Description = "Скорость ротора счётчика стала нормальной", Description_45 = "Скорость ротора счётчика стала нормальной" },
                new FloutecAlarmTypes { Code = 146, Description = "Скорость ротора счётчика стала выше допустимой", Description_45 = "Скорость ротора счётчика стала выше допустимой" },
                new FloutecAlarmTypes { Code = 19, Description = "Условия для расчёта F стали нормальными", Description_45 = "Условия для расчёта F стали нормальными" },
                new FloutecAlarmTypes { Code = 147, Description = "Условия для расчёта F стали ненормальными", Description_45 = "Условия для расчёта F стали ненормальными" },
                new FloutecAlarmTypes { Code = 20, Description = "Конец использования нижнего ПД", Description_45 = "Конец использования дифманометра нижнего перепада" },
                new FloutecAlarmTypes { Code = 148, Description = "Начало использования нижнего ПД", Description_45 = "Начало использования дифманометра нижнего перепада" },
                new FloutecAlarmTypes { Code = 21, Description = "Изменена калибровка канала", Description_45 = "Изменена калибровка канала" },
                new FloutecAlarmTypes { Code = 22, Description = "ПДВ ниже верхнего предела измерений", Description_45 = "ПДВ ниже верхнего предела измерений" },
                new FloutecAlarmTypes { Code = 150, Description = "ПДВ выше верхнего предела измерений", Description_45 = "ПДВ выше верхнего предела измерений" },
                new FloutecAlarmTypes { Code = 23, Description = "Давление стало ниже верхнего предела измерений", Description_45 = "Давление стало ниже верхнего предела измерений" },
                new FloutecAlarmTypes { Code = 151, Description = "Давление стало выше верхнего предела измерений", Description_45 = "Давление стало выше верхнего предела измерений" },
                new FloutecAlarmTypes { Code = 24, Description = "Температура стала выше -25 °С", Description_45 = "Температура стала выше -25 °С" },
                new FloutecAlarmTypes { Code = 152, Description = "Температура стала ниже -25 °С", Description_45 = "Температура стала ниже -25 °С" },
                new FloutecAlarmTypes { Code = 25, Description = "Абсолютное давление стало ниже или равно ВМП", Description_45 = "Абсолютное давление стало ниже или равно ВМП" },
                new FloutecAlarmTypes { Code = 153, Description = "Абсолютное давление стало выше ВМП", Description_45 = "Абсолютное давление стало выше ВМП" },
                new FloutecAlarmTypes { Code = 26, Description = "Температура стала выше -30 °С", Description_45 = "Температура стала выше -30 °С" },
                new FloutecAlarmTypes { Code = 154, Description = "Температура стала ниже -30 °С", Description_45 = "Температура стала ниже -30 °С" },
                new FloutecAlarmTypes { Code = 27, Description = "Коэффициент Ксж в норме, конец замены предыдущим значением", Description_45 = "Коэффициент Ксж в норме, конец замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 155, Description = "Коэффициент Ксж меньше 0, начало замены предыдущим значением", Description_45 = "Коэффициент Ксж меньше 0, начало замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 28, Description = "Абсолютное давление выше минимального атмосферного давления, конец замены предыдущим значением", Description_45 = "Абсолютное давление выше минимального атмосферного давления, конец замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 156, Description = "Абсолютное давление ниже минимального атмосферного давления, начало замены предыдущим значением", Description_45 = "Абсолютное давление ниже минимального атмосферного давления, начало замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 29, Description = "Вязкость в норме, конец замены предыдущим значением", Description_45 = "Вязкость в норме, конец замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 157, Description = "Вязкость не в норме, начало замены предыдущим значением", Description_45 = "Вязкость не в норме, начало замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 30, Description = "Значение не NAN, конец замены предыдущим значением", Description_45 = "Значение не NAN, конец замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 158, Description = "Значение NAN, начало замены предыдущим значением", Description_45 = "Значение NAN, начало замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 31, Description = "Значение меньше максимально допустимого, конец замены предыдущим значением", Description_45 = "Значение меньше максимально допустимого, конец замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 159, Description = "Значение выше максимально допустимого, начало замены предыдущим значением", Description_45 = "Значение выше максимально допустимого, начало замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 32, Description = "Значение выше минимально допустимого, конец замены предыдущим значением", Description_45 = "Значение выше минимально допустимого, конец замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 160, Description = "Значение ниже минимально допустимого, начало замены предыдущим значением", Description_45 = "Значение ниже минимально допустимого, начало замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 33, Description = "Конец обратного потока, конец замены предыдущим значением", Description_45 = "Конец обратного потока, конец замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 161, Description = "Начало обратного потока, начало замены предыдущим значением", Description_45 = "Начало обратного потока, начало замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 34, Description = "Единицы измерения в норме, конец замены предыдущим значением", Description_45 = "Единицы измерения в норме, конец замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 162, Description = "Единицы измерения не в норме, начало замены предыдущим значением", Description_45 = "Единицы измерения не в норме, начало замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 149, Description = "Ошибка опроса датчика", Description_45 = "Ошибка опроса датчика" },
                new FloutecAlarmTypes { Code = 39, Description = "ПДН стал ниже верхнего предела измерений", Description_45 = "ПДН стал ниже верхнего предела измерений" },
                new FloutecAlarmTypes { Code = 167, Description = "ПДН стал выше верхнего предела измерений", Description_45 = "ПДН стал выше верхнего предела измерений" },
                new FloutecAlarmTypes { Code = 40, Description = "Подано напряжение питания", Description_45 = "Подано напряжение питания" },
                new FloutecAlarmTypes { Code = 168, Description = "Снято напряжение питания", Description_45 = "Снято напряжение питания" },
                new FloutecAlarmTypes { Code = 41, Description = "Конфигурирование вычислителя", Description_45 = "Конфигурирование вычислителя" },
                new FloutecAlarmTypes { Code = 42, Description = "Абсолютное давление стало выше или равно НМП", Description_45 = "Абсолютное давление стало выше или равно НМП" },
                new FloutecAlarmTypes { Code = 170, Description = "Абсолютное давление стало ниже НМП", Description_45 = "Абсолютное давление стало ниже НМП" },
                new FloutecAlarmTypes { Code = 43, Description = "Температура стала ниже 66.85 °С", Description_45 = "Температура стала ниже 66.85 °С" },
                new FloutecAlarmTypes { Code = 171, Description = "Температура стала выше 66.85 °С", Description_45 = "Температура стала выше 66.85 °С" },
                new FloutecAlarmTypes { Code = 44, Description = "Температура стала выше -23.15 °С", Description_45 = "Температура стала выше -23.15 °С" },
                new FloutecAlarmTypes { Code = 172, Description = "Температура стала ниже -23.15 °С", Description_45 = "Температура стала ниже -23.15 °С" },
                new FloutecAlarmTypes { Code = 35, Description = "Установка нуля канала", Description_45 = "Установка нуля канала" },
                new FloutecAlarmTypes { Code = 36, Description = "Абсолютное давление стало ниже 100 кгс/см2", Description_45 = "Абсолютное давление стало ниже 100 кгс/см2" },
                new FloutecAlarmTypes { Code = 164, Description = "Абсолютное давление стало выше 100 кгс/см2", Description_45 = "Абсолютное давление стало выше 100 кгс/см2" },
                new FloutecAlarmTypes { Code = 37, Description = "Конец режима 'заморозки'", Description_45 = "Конец режима 'заморозки'" },
                new FloutecAlarmTypes { Code = 165, Description = "Начало режима 'заморозки'", Description_45 = "Начало режима 'заморозки'" },
                new FloutecAlarmTypes { Code = 38, Description = "Конец использования НСХП канала", Description_45 = "Конец использования НСХП канала" },
                new FloutecAlarmTypes { Code = 166, Description = "Начало использования НСХП канала", Description_45 = "Начало использования НСХП канала" },
                new FloutecAlarmTypes { Code = 45, Description = "Расчёт плотности в норме, конец замены предыдущим значением", Description_45 = "Расчёт плотности в норме, конец замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 173, Description = "Расчёт плотности не в норме, начало замены предыдущим значением", Description_45 = "Расчёт плотности не в норме, начало замены предыдущим значением" },
                new FloutecAlarmTypes { Code = 50, Description = "Температура сенсора SMV стала нормальной", Description_45 = "Температура сенсора SMV стала нормальной" },
                new FloutecAlarmTypes { Code = 178, Description = "Температура сенсора SMV стала выше нормы", Description_45 = "Температура сенсора SMV стала выше нормы" },
                new FloutecAlarmTypes { Code = 51, Description = "Температура стала ниже 40 °С", Description_45 = "Температура стала ниже 40 °С" },
                new FloutecAlarmTypes { Code = 179, Description = "Температура стала выше 40 °С", Description_45 = "Температура стала выше 40 °С" },
                new FloutecAlarmTypes { Code = 52, Description = "Температура стала выше -10 °С", Description_45 = "Температура стала выше -10 °С" },
                new FloutecAlarmTypes { Code = 180, Description = "Температура стала ниже -10 °С", Description_45 = "Температура стала ниже -10 °С" },
                new FloutecAlarmTypes { Code = 53, Description = "Абсолютное давление стало выше 11 кгс/см2", Description_45 = "Абсолютное давление стало выше 11 кгс/см2" },
                new FloutecAlarmTypes { Code = 181, Description = "Абсолютное давление стало ниже 11 кгс/см2", Description_45 = "Абсолютное давление стало ниже 11 кгс/см2" },
                new FloutecAlarmTypes { Code = 54, Description = "Абсолютное давление стало ниже 26 кгс/см2", Description_45 = "Абсолютное давление стало ниже 26 кгс/см2" },
                new FloutecAlarmTypes { Code = 182, Description = "Абсолютное давление стало выше 26 кгс/см2", Description_45 = "Абсолютное давление стало выше 26 кгс/см2" },
                new FloutecAlarmTypes { Code = 55, Description = "Значение стало выше НМП", Description_45 = "Значение стало выше НМП" },
                new FloutecAlarmTypes { Code = 183, Description = "Значение стало ниже НМП", Description_45 = "Значение стало ниже НМП" },
                new FloutecAlarmTypes { Code = 56, Description = "Значение стало ниже ВМП", Description_45 = "Значение стало ниже ВМП" },
                new FloutecAlarmTypes { Code = 184, Description = "Значение стало выше ВМП", Description_45 = "Значение стало выше ВМП" },
                new FloutecAlarmTypes { Code = 57, Description = "Значение стало выше НПИ", Description_45 = "Значение стало выше НПИ" },
                new FloutecAlarmTypes { Code = 185, Description = "Значение стало ниже НПИ", Description_45 = "Значение стало ниже НПИ" },
                new FloutecAlarmTypes { Code = 58, Description = "Значение стало ниже ВПИ", Description_45 = "Значение стало ниже ВПИ" },
                new FloutecAlarmTypes { Code = 186, Description = "Значение стало выше ВПИ", Description_45 = "Значение стало выше ВПИ" },
                new FloutecAlarmTypes { Code = 8, Description = "Расчёт по ГОСТ 8.586 стал возможным", Description_45 = "Корректный расчёт Кш стал возможным" },
                new FloutecAlarmTypes { Code = 136, Description = "Расчёт по ГОСТ 8.586 стал невозможным", Description_45 = "Корректный расчёт Кш стал невозможным" },
                new FloutecAlarmTypes { Code = 9, Description = "Запись во FLASH-память не выполнена", Description_45 = "Запись во FLASH-память не выполнена" },
                new FloutecAlarmTypes { Code = 137, Description = "Запись в буфер FLASH-памяти не выполнена", Description_45 = "Запись в буфер FLASH-памяти не выполнена" },
                new FloutecAlarmTypes { Code = 46, Description = "Аккумулятор КВВ в норме", Description_45 = "Аккумулятор КВВ в норме" },
                new FloutecAlarmTypes { Code = 174, Description = "Аккумулятор КВВ разряжен", Description_45 = "Аккумулятор КВВ разряжен" },
                new FloutecAlarmTypes { Code = 59, Description = "Расчётная частота на импульсном выходе в норме", Description_45 = "Расчётная частота на импульсном выходе в норме" },
                new FloutecAlarmTypes { Code = 187, Description = "Расчётная частота на импульсном выходе выше максимально допустимой", Description_45 = "Расчётная частота на импульсном выходе выше максимально допустимой" },
                new FloutecAlarmTypes { Code = 60, Description = "Карта памяти присутствует", Description_45 = "Карта памяти присутствует" },
                new FloutecAlarmTypes { Code = 188, Description = "Карта памяти отсутствует", Description_45 = "Карта памяти отсутствует" },
                new FloutecAlarmTypes { Code = 61, Description = "Карта памяти исправна", Description_45 = "Карта памяти исправна" },
                new FloutecAlarmTypes { Code = 189, Description = "Карта памяти неисправна", Description_45 = "Карта памяти неисправна" },
                new FloutecAlarmTypes { Code = 62, Description = "Карта памяти подготовлена", Description_45 = "Карта памяти подготовлена" },
                new FloutecAlarmTypes { Code = 190, Description = "Карта памяти не подготовлена", Description_45 = "Карта памяти не подготовлена" },
                new FloutecAlarmTypes { Code = 63, Description = "Обнаружен повреждённый кластер", Description_45 = "Обнаружен повреждённый кластер" },
                new FloutecAlarmTypes { Code = 191, Description = "Все кластеры в норме", Description_45 = "Все кластеры в норме" },
                new FloutecAlarmTypes { Code = 64, Description = "Запись во FLASH-память выполнена", Description_45 = "Запись во FLASH-память выполнена" },
                new FloutecAlarmTypes { Code = 192, Description = "Запись во FLASH-память не выполнена", Description_45 = "Запись во FLASH-память не выполнена" },
                new FloutecAlarmTypes { Code = 47, Description = "Значение расхода при р.у. стало выше значения, при котором счётчик останавливается", Description_45 = "Значение расхода при р.у. стало выше значения, при котором счётчик останавливается" },
                new FloutecAlarmTypes { Code = 175, Description = "Значение расхода при р.у. стало ниже значения, при котором счётчик останавливается", Description_45 = "Значение расхода при р.у. стало ниже значения, при котором счётчик останавливается" },
                new FloutecAlarmTypes { Code = 65, Description = "Значение скорости потока стало ниже максимально допустимого", Description_45 = " " },
                new FloutecAlarmTypes { Code = 193, Description = "Значение скорости потока стало выше максимально допустимого", Description_45 = " " },
                new FloutecAlarmTypes { Code = 66, Description = "Опрос ультразвукового счётчика в норме", Description_45 = " " },
                new FloutecAlarmTypes { Code = 194, Description = "Опрос ультразвукового счётчика не в норме", Description_45 = " " },
                new FloutecAlarmTypes { Code = 67, Description = "Изменена карта регистров ошибок", Description_45 = " " },
                new FloutecAlarmTypes { Code = 195, Description = "Ультразвуковой счётчик: неожиданное значение объёма", Description_45 = " " },
                new FloutecAlarmTypes { Code = 68, Description = "Ультразвуковой счётчик: неожиданное значение аварийного объёма", Description_45 = " " },
                new FloutecAlarmTypes { Code = 48, Description = "Проток одоранта в норме", Description_45 = "Проток одоранта в норме" },
                new FloutecAlarmTypes { Code = 176, Description = "Проток одоранта отсутствует", Description_45 = "Проток одоранта отсутствует" },
                new FloutecAlarmTypes { Code = 49, Description = "ЭППЗУ КВВ в норме", Description_45 = "ЭППЗУ КВВ в норме" },
                new FloutecAlarmTypes { Code = 177, Description = "ЭППЗУ КВВ исчерпало ресурс", Description_45 = "ЭППЗУ КВВ исчерпало ресурс" },
                new FloutecAlarmTypes { Code = 163, Description = "Изменено смещение в канале", Description_45 = "Изменено смещение в канале" },
                new FloutecAlarmTypes { Code = 169, Description = "", Description_45 = "Изменены параметры АЦП" }
            };

            if (!floutecAlarmTypes.Any())
            {
                floutecAlarmTypes.ForEach(a => { context.FloutecAlarmTypes.AddOrUpdate(a); });
            }            

            List<FloutecInterTypes> floutecInterTypes = new List<FloutecInterTypes>
            {
                new FloutecInterTypes { Code = 0, Description = "Наименование трубопровода", Description_45 = "Наименование трубопровода" },
                new FloutecInterTypes { Code = 1, Description = "Плотность, кг/м3", Description_45 = "Плотность, кг/м3" },
                new FloutecInterTypes { Code = 2, Description = "Молярная доля СО2, %", Description_45 = "Молярная доля СО2, %" },
                new FloutecInterTypes { Code = 3, Description = "Молярная доля N2, %", Description_45 = "Молярная доля N2, %" },
                new FloutecInterTypes { Code = 4, Description = "Диаметр трубопровода, мм", Description_45 = "Диаметр трубопровода, мм" },
                new FloutecInterTypes { Code = 5, Description = "Диаметр СУ, мм", Description_45 = "Диаметр СУ, мм" },
                new FloutecInterTypes { Code = 6, Description = "Атмосферное давление, мм рт. ст.", Description_45 = "Атмосферное давление, мм рт. ст." },
                new FloutecInterTypes { Code = 7, Description = "Отсечка ПД, кг/м2", Description_45 = "Отсечка ПД, кг/м2" },
                new FloutecInterTypes { Code = 8, Description = "Минимальный расход (Qmin), м3/час", Description_45 = "Минимальный расход (Qmin), м3/час" },
                new FloutecInterTypes { Code = 9, Description = "ВПИ расхода (Qmax), м3/час", Description_45 = "ВПИ расхода (Qmax), м3/час" },
                new FloutecInterTypes { Code = 10, Description = "Наименование объекта", Description_45 = "Наименование объекта" },
                new FloutecInterTypes { Code = 11, Description = "Порог переключения перепада давления, кг/м2", Description_45 = "Порог переключения перепада давления, кг/м2" },
                new FloutecInterTypes { Code = 12, Description = "Тип отбора перепада давления", Description_45 = "Тип отбора перепада давления" },
                new FloutecInterTypes { Code = 13, Description = "Удельная теплота сгорания", Description_45 = "Удельная теплота сгорания" },
                new FloutecInterTypes { Code = 14, Description = "Объём газа на 1 выходной импульс, м3", Description_45 = "Объём газа на 1 выходной импульс, м3" },
                new FloutecInterTypes { Code = 15, Description = "Изменены параметры доступа пользователя", Description_45 = "Калибровка нуля перепада давления" },
                new FloutecInterTypes { Code = 16, Description = "Калибровка нуля нижнего перепада давления", Description_45 = "Калибровка нуля нижнего перепада давления" },
                new FloutecInterTypes { Code = 17, Description = "Номер скважины", Description_45 = "Калибровка нуля верхнего перепада давления" },
                new FloutecInterTypes { Code = 18, Description = "Вязкость конденсата, мкН*с/м2", Description_45 = "Вязкость конденсата, мкН*с/м2" },
                new FloutecInterTypes { Code = 19, Description = "Шероховатость трубы, мм", Description_45 = "Шероховатость трубы, мм" },
                new FloutecInterTypes { Code = 20, Description = "Коэффициент Ае (а0*1е-06) для расчёта Ктлр СУ", Description_45 = "Коэффициент Ае (а0*1е-06) для расчёта Ктлр СУ" },
                new FloutecInterTypes { Code = 21, Description = "Коэффициент Ве (а1*1е-09) для расчёта Ктлр СУ", Description_45 = "Коэффициент Ве (а1*1е-09) для расчёта Ктлр СУ" },
                new FloutecInterTypes { Code = 22, Description = "Ктлр материала диафрагмы", Description_45 = "Ктлр материала диафрагмы" },
                new FloutecInterTypes { Code = 23, Description = "Коэффициент Се (а2*1е-12) для расчёта Ктлр СУ", Description_45 = "Коэффициент Се (а2*1е-12) для расчёта Ктлр СУ" },
                new FloutecInterTypes { Code = 24, Description = "Ктлр материала трубы", Description_45 = "Ктлр материала трубы" },
                new FloutecInterTypes { Code = 25, Description = "Минимально допустимое напряжение №1", Description_45 = "Минимально допустимое напряжение №1" },
                new FloutecInterTypes { Code = 26, Description = "Минимально допустимое напряжение №2", Description_45 = "Минимально допустимое напряжение №2" },
                new FloutecInterTypes { Code = 27, Description = "Минимально допустимое напряжение №3", Description_45 = "Минимально допустимое напряжение №3" },
                new FloutecInterTypes { Code = 28, Description = "Когда на летнее время", Description_45 = "Когда на летнее время" },
                new FloutecInterTypes { Code = 29, Description = "Когда на зимнее время", Description_45 = "Когда на зимнее время" },
                new FloutecInterTypes { Code = 30, Description = "Время - переход на летнее", Description_45 = "Время - переход на летнее" },
                new FloutecInterTypes { Code = 31, Description = "Время - переход на зимнее", Description_45 = "Время - переход на зимнее" },
                new FloutecInterTypes { Code = 32, Description = "Оперативный интревал", Description_45 = "Оперативный интревал" },
                new FloutecInterTypes { Code = 33, Description = "Коэффициент Ае (а0*1е-06) для расчёта Ктлр трубы", Description_45 = "Коэффициент Ае (а0*1е-06) для расчёта Ктлр трубы" },
                new FloutecInterTypes { Code = 34, Description = "Коэффициент Ве (а1*1е-09) для расчёта Ктлр трубы", Description_45 = "Коэффициент Ве (а1*1е-09) для расчёта Ктлр трубы" },
                new FloutecInterTypes { Code = 35, Description = "Коэффициент Се (а2*1е-12) для расчёта Ктлр трубы", Description_45 = "Коэффициент Се (а2*1е-12) для расчёта Ктлр трубы" },
                new FloutecInterTypes { Code = 36, Description = "Счётчик десятков млн. м3 объёма при р.у.", Description_45 = "Счётчик объёмов по 10 тыс. м3 при р.у. по модулю 10000000" },
                new FloutecInterTypes { Code = 37, Description = "Объём при р.у., м3", Description_45 = "Объём при р.у. по модулю 10000, м3" },
                new FloutecInterTypes { Code = 38, Description = "Тип ОНТ", Description_45 = "Тип ОНТ" },
                new FloutecInterTypes { Code = 39, Description = "Модель ОНТ ITABAR", Description_45 = "Модель ОНТ ITABAR" },
                new FloutecInterTypes { Code = 40, Description = "Модель ОНТ ANNUBAR", Description_45 = "Ширина зонда в свете, мм" },
                new FloutecInterTypes { Code = 41, Description = "Единица измерений давления", Description_45 = "Единица измерений давления" },
                new FloutecInterTypes { Code = 42, Description = "Единица измерений перепада давления", Description_45 = "Единица измерений перепада давления" },
                new FloutecInterTypes { Code = 43, Description = "Единица измерений атмосферного давления", Description_45 = "Единица измерений атмосферного давления" },
                new FloutecInterTypes { Code = 44, Description = "Тип статического давления", Description_45 = "Тип статического давления" },
                new FloutecInterTypes { Code = 45, Description = "Необходимость пароля для чтения данных", Description_45 = "Необходимость пароля для чтения данных" },
                new FloutecInterTypes { Code = 46, Description = "Период калибровки АЦП при работе", Description_45 = "Период калибровки АЦП при работе" },
                new FloutecInterTypes { Code = 47, Description = "Период калибровки АЦП при калибровке", Description_45 = "Период калибровки АЦП при калибровке" },
                new FloutecInterTypes { Code = 48, Description = "ВПИ перепада давления, кГс/м2", Description_45 = "ВПИ перепада давления, кГс/м2" },
                new FloutecInterTypes { Code = 49, Description = "НПИ давления, кГс/см2", Description_45 = "НПИ давления, кГс/см2" },
                new FloutecInterTypes { Code = 50, Description = "ВПИ давления, кГс/см2", Description_45 = "ВПИ давления, кГс/см2" },
                new FloutecInterTypes { Code = 51, Description = "НПИ температуры, °С", Description_45 = "НПИ температуры, °С" },
                new FloutecInterTypes { Code = 52, Description = "ВПИ температуры, °С", Description_45 = "ВПИ температуры, °С" },
                new FloutecInterTypes { Code = 53, Description = "Количество импульсов на 1 м3", Description_45 = "Количество импульсов на 1 м3" },
                new FloutecInterTypes { Code = 54, Description = "Максимально возможное давление, кГс/см2", Description_45 = "Максимально возможное давление, кГс/см2" },
                new FloutecInterTypes { Code = 55, Description = "Расход, при котором счётчик останавливается, м3/час", Description_45 = "Расход, при котором счётчик останавливается, м3/час" },
                new FloutecInterTypes { Code = 56, Description = "Максимально допускаемый расход при р.у., м3/час", Description_45 = "Максимально допускаемый расход при р.у., м3/час" },
                new FloutecInterTypes { Code = 57, Description = "Максимально возможный перепад давления, кГс/м2", Description_45 = "Максимально возможный перепад давления, кГс/м2" },
                new FloutecInterTypes { Code = 58, Description = "НПИ перепада давления, кГс/м2", Description_45 = "НПИ перепада давления, кГс/м2" },
                new FloutecInterTypes { Code = 59, Description = "НПИ плотности", Description_45 = "НПИ плотности" },
                new FloutecInterTypes { Code = 60, Description = "ВПИ плотности", Description_45 = "ВПИ плотности" },
                new FloutecInterTypes { Code = 61, Description = "Использование НСХП цифрового канала Д", Description_45 = "Использование НСХП цифрового канала Д" },
                new FloutecInterTypes { Code = 62, Description = "Использование НСХП цифрового канала Т", Description_45 = "Использование НСХП цифрового канала Т" },
                new FloutecInterTypes { Code = 63, Description = "Использование НСХП цифрового канала ПД", Description_45 = "Использование НСХП цифрового канала ПД" },
                new FloutecInterTypes { Code = 64, Description = "Использование НСХП цифрового канала ПДН", Description_45 = "Использование НСХП цифрового канала ПДН" },
                new FloutecInterTypes { Code = 65, Description = "Санкционированное использование НСХП цифрового канала Д", Description_45 = "Санкционированное использование НСХП цифрового канала Д" },
                new FloutecInterTypes { Code = 66, Description = "Санкционированное использование НСХП цифрового канала Т", Description_45 = "Санкционированное использование НСХП цифрового канала Т" },
                new FloutecInterTypes { Code = 67, Description = "Санкционированное использование НСХП цифрового канала ПД", Description_45 = "Санкционированное использование НСХП цифрового канала ПД" },
                new FloutecInterTypes { Code = 68, Description = "Санкционированное использование НСХП цифрового канала ПДН", Description_45 = "Санкционированное использование НСХП цифрового канала ПДН" },
                new FloutecInterTypes { Code = 69, Description = "Несанкционированное использование НСХП цифрового канала Д", Description_45 = "Несанкционированное использование НСХП цифрового канала Д" },
                new FloutecInterTypes { Code = 70, Description = "Несанкционированное использование НСХП цифрового канала Т", Description_45 = "Несанкционированное использование НСХП цифрового канала Т" },
                new FloutecInterTypes { Code = 71, Description = "Несанкционированное использование НСХП цифрового канала ПД", Description_45 = "Несанкционированное использование НСХП цифрового канала ПД" },
                new FloutecInterTypes { Code = 72, Description = "Несанкционированное использование НСХП цифрового канала ПДН", Description_45 = "Несанкционированное использование НСХП цифрового канала ПДН" },
                new FloutecInterTypes { Code = 128, Description = "Время", Description_45 = "Время" },
                new FloutecInterTypes { Code = 129, Description = "Калибровочный коэффициент ОНТ", Description_45 = "Калибровочный коэффициент ОНТ" },
                new FloutecInterTypes { Code = 130, Description = "Период цикла опроса, сек.", Description_45 = "Период цикла опроса, сек." },
                new FloutecInterTypes { Code = 131, Description = "Контрактный час", Description_45 = "Контрактный час" },
                new FloutecInterTypes { Code = 132, Description = "Минимально допускаемое число Re", Description_45 = "Минимально допускаемое число Re" },
                new FloutecInterTypes { Code = 133, Description = "Параметры НСХП канала Д", Description_45 = "Параметры НСХП канала Д" },
                new FloutecInterTypes { Code = 134, Description = "Параметры НСХП канала Т", Description_45 = "Параметры НСХП канала Т" },
                new FloutecInterTypes { Code = 135, Description = "Параметры НСХП канала ПД", Description_45 = "Параметры НСХП канала ПД" },
                new FloutecInterTypes { Code = 136, Description = "Параметры НСХП канала ПДВ", Description_45 = "Параметры НСХП канала ПДВ" },
                new FloutecInterTypes { Code = 137, Description = "Параметры НСХП канала ПДН", Description_45 = "Параметры НСХП канала ПДН" },
                new FloutecInterTypes { Code = 138, Description = "Ширина зонда в свету, мм", Description_45 = "Константа коэффициента расширения" },
                new FloutecInterTypes { Code = 139, Description = "Метод измерений", Description_45 = "Метод измерений" },
                new FloutecInterTypes { Code = 141, Description = "Управление одорантом", Description_45 = "Управление одорантом" },
                new FloutecInterTypes { Code = 142, Description = "Начальный радиус входной кромки диафрагмы, мм", Description_45 = "Начальный радиус входной кромки диафрагмы, мм" },
                new FloutecInterTypes { Code = 143, Description = "Межконтрольный интервал СУ", Description_45 = "Межконтрольный интервал СУ" },
                new FloutecInterTypes { Code = 144, Description = "Постановка на константу", Description_45 = "Постановка на константу" },
                new FloutecInterTypes { Code = 145, Description = "Снятие с константы", Description_45 = "Снятие с константы" },
                new FloutecInterTypes { Code = 146, Description = "Параметры калибровки", Description_45 = "Параметры калибровки" },
                new FloutecInterTypes { Code = 147, Description = "Смещение ПД", Description_45 = "Смещение ПД" },
                new FloutecInterTypes { Code = 148, Description = "Коэффициент наклона ПД", Description_45 = "Коэффициент наклона ПД" },
                new FloutecInterTypes { Code = 149, Description = "Смещение Т", Description_45 = "Смещение Д" },
                new FloutecInterTypes { Code = 150, Description = "Коэффициент наклона Т", Description_45 = "Коэффициент наклона Д" },
                new FloutecInterTypes { Code = 151, Description = "Установка нуля", Description_45 = "Установка нуля" },
                new FloutecInterTypes { Code = 152, Description = "Параметры АЦП", Description_45 = "Параметры АЦП" },
                new FloutecInterTypes { Code = 153, Description = "Константа Д (санкционированное изменение)", Description_45 = "Константа Д (санкционированное изменение)" },
                new FloutecInterTypes { Code = 154, Description = "Константа Т (санкционированное изменение)", Description_45 = "Константа Т (санкционированное изменение)" },
                new FloutecInterTypes { Code = 155, Description = "Константа (санкционированное изменение)", Description_45 = "Константа (санкционированное изменение)" },
                new FloutecInterTypes { Code = 156, Description = "Константа П (санкционированное изменение)", Description_45 = "Константа П (санкционированное изменение)" },
                new FloutecInterTypes { Code = 157, Description = "Константа Д (несанкционированное изменение)", Description_45 = "Константа Д (несанкционированное изменение)" },
                new FloutecInterTypes { Code = 158, Description = "Константа Т (несанкционированное изменение)", Description_45 = "Константа Т (несанкционированное изменение)" },
                new FloutecInterTypes { Code = 159, Description = "Константа (несанкционированное изменение)", Description_45 = "Константа (несанкционированное изменение)" },
                new FloutecInterTypes { Code = 160, Description = "Константа П (несанкционированное изменение)", Description_45 = "Константа П (несанкционированное изменение)" },
                new FloutecInterTypes { Code = 161, Description = "Норма одоранта, мг/нм3", Description_45 = "Норма одоранта, мг/нм3" },
                new FloutecInterTypes { Code = 162, Description = "Максимальная доза одоранта, см3", Description_45 = "Максимальная доза одоранта, см3" },
                new FloutecInterTypes { Code = 168, Description = "Объём одоранта, соответствующий одному циклу", Description_45 = "Объём камеры расходомера" },
                new FloutecInterTypes { Code = 169, Description = "Отсчёт по шкале дозатора", Description_45 = "Отсчёт по шкале дозатора" },
                new FloutecInterTypes { Code = 170, Description = "Плотность одоранта, г/см3", Description_45 = "Плотность одоранта, г/см3" },
                new FloutecInterTypes { Code = 171, Description = "Концентрация меркаптановой серы, г/тыс. м3", Description_45 = "Концентрация меркаптановой серы, г/тыс. м3" },
                new FloutecInterTypes { Code = 172, Description = "Молярная доля метана СН4, %", Description_45 = "Молярная доля метана СН4, %" },
                new FloutecInterTypes { Code = 173, Description = "Молярная доля этана С2Н6, %", Description_45 = "Молярная доля этана С2Н6, %" },
                new FloutecInterTypes { Code = 174, Description = "Молярная доля пропана С3Н8, %", Description_45 = "Молярная доля пропана С3Н8, %" },
                new FloutecInterTypes { Code = 175, Description = "Молярная доля бутана С4Н10, %", Description_45 = "Молярная доля бутана С4Н10, %" },
                new FloutecInterTypes { Code = 176, Description = "Молярная доля пентана С5Н12, %", Description_45 = "Молярная доля пентана С5Н12, %" },
                new FloutecInterTypes { Code = 177, Description = "Молярная доля сероводорода H2S, %", Description_45 = "Молярная доля сероводорода H2S, %" },
                new FloutecInterTypes { Code = 178, Description = "Параметры преобразователя", Description_45 = "Параметры преобразователя" },
                new FloutecInterTypes { Code = 179, Description = "Постановка на константу", Description_45 = "Постановка на константу" },
                new FloutecInterTypes { Code = 180, Description = "Снятие с константы", Description_45 = "Снятие с константы" },
                new FloutecInterTypes { Code = 181, Description = "Константа Д", Description_45 = "Константа Д" },
                new FloutecInterTypes { Code = 182, Description = "Константа Т", Description_45 = "Константа Т" },
                new FloutecInterTypes { Code = 183, Description = "Константа", Description_45 = "Константа" },
                new FloutecInterTypes { Code = 184, Description = "Константа П", Description_45 = "Константа П" },
                new FloutecInterTypes { Code = 185, Description = "Максимально допускаемая скорость потока, м/с", Description_45 = "" },
                new FloutecInterTypes { Code = 186, Description = "Адрес регистров расхода", Description_45 = "" },
                new FloutecInterTypes { Code = 187, Description = "Адрес регистров объёма", Description_45 = "" },
                new FloutecInterTypes { Code = 188, Description = "Адрес регистров скорости потока", Description_45 = "" },
                new FloutecInterTypes { Code = 189, Description = "Порядок байтов по протоколу MODBUS", Description_45 = "" },
                new FloutecInterTypes { Code = 190, Description = "Режим работы по протоколу MODBUS", Description_45 = "" },
                new FloutecInterTypes { Code = 191, Description = "Адрес регистра разрешения счётчика", Description_45 = "" },
                new FloutecInterTypes { Code = 192, Description = "Адрес регистра аварийного объёма", Description_45 = "" },
                new FloutecInterTypes { Code = 193, Description = "Адрес ультразвукового счётчика", Description_45 = "" },
                new FloutecInterTypes { Code = 194, Description = "Порядок байтов переменных в ответе функции 6", Description_45 = "" },
                new FloutecInterTypes { Code = 195, Description = "Конфигурация импульсного выхода №1", Description_45 = "" },
                new FloutecInterTypes { Code = 196, Description = "Конфигурация импульсного выхода №2", Description_45 = "" },
                new FloutecInterTypes { Code = 197, Description = "Конфигурация импульсного выхода №3", Description_45 = "" },
                new FloutecInterTypes { Code = 198, Description = "Длинный адрес ЦП", Description_45 = "" },
                new FloutecInterTypes { Code = 199, Description = "Адрес вычислителя", Description_45 = "" },
                new FloutecInterTypes { Code = 200, Description = "Счётчик десятков млн. м3 в показаниях счётчика по обратному потоку", Description_45 = "" },
                new FloutecInterTypes { Code = 201, Description = "Объём (меньше 10 млн. м3) в показаниях счётчика по обратному потоку", Description_45 = "" },
                new FloutecInterTypes { Code = 202, Description = "Конфигурация линий подключения ЦП", Description_45 = "" },
                new FloutecInterTypes { Code = 203, Description = "Объём газа на 1 импульс для импульсного выхода №1, м3", Description_45 = "" },
                new FloutecInterTypes { Code = 204, Description = "Объём газа на 1 импульс для импульсного выхода №2, м3", Description_45 = "" },
                new FloutecInterTypes { Code = 205, Description = "Объём газа на 1 импульс для импульсного выхода №3, м3", Description_45 = "" }
            };

            if (!floutecInterTypes.Any())
            {
                floutecInterTypes.ForEach(i => { context.FloutecInterTypes.AddOrUpdate(i); });
            }            

            List<FloutecSensorTypes> floutecSensorTypes = new List<FloutecSensorTypes>
            {
                new FloutecSensorTypes { Code = 1, Description = "Диафрагма" },
                new FloutecSensorTypes { Code = 2, Description = "Счётчик" },
                new FloutecSensorTypes { Code = 3, Description = "Массовый расходомер" },
            };

            if (!floutecSensorTypes.Any())
            {
                floutecSensorTypes.ForEach(s => { context.FloutecSensorTypes.AddOrUpdate(s); });
            }            

            List<Roc809EventTypes> rocEventTypes = new List<Roc809EventTypes>
            {
                new Roc809EventTypes { Code = 0, Description = "Событие отсутствует" },
                new Roc809EventTypes { Code = 1, Description = "Событие изменения параметра" },
                new Roc809EventTypes { Code = 2, Description = "Системное событие" },
                new Roc809EventTypes { Code = 3, Description = "Событие таблицы последовательности функций (FST)" },
                new Roc809EventTypes { Code = 4, Description = "Пользовательское событие" },
                new Roc809EventTypes { Code = 5, Description = "Событие потери питания" },
                new Roc809EventTypes { Code = 6, Description = "Событие установки часов" },
                new Roc809EventTypes { Code = 7, Description = "Событие проверки калибровки" }
            };

            if (!rocEventTypes.Any())
            {
                rocEventTypes.ForEach(e => { context.Roc809EventTypes.AddOrUpdate(e); });
            }           

            List<Roc809EventCodes> rocEventCodes = new List<Roc809EventCodes>
            {
                new Roc809EventCodes { Code = 144, Description = "Последовательность инициализации" },
                new Roc809EventCodes { Code = 145, Description = "Отключено всё питание" },
                new Roc809EventCodes { Code = 146, Description = "Инициализация значениями по умолчанию" },
                new Roc809EventCodes { Code = 147, Description = "Ошибка контрольной суммы ПЗУ" },
                new Roc809EventCodes { Code = 148, Description = "Инициализация базы данных" },
                new Roc809EventCodes { Code = 150, Description = "Программирование FLASH-памяти" },
                new Roc809EventCodes { Code = 151, Description = "Зарезервировано для ROC809" },
                new Roc809EventCodes { Code = 152, Description = "Зарезервировано для ROC809" },
                new Roc809EventCodes { Code = 153, Description = "Зарезервировано для ROC809" },
                new Roc809EventCodes { Code = 154, Description = "Добавлен SMART-модуль" },
                new Roc809EventCodes { Code = 155, Description = "Удалён SMART-модуль" },
                new Roc809EventCodes { Code = 200, Description = "Установка часов" },
                new Roc809EventCodes { Code = 248, Description = "Текстовое сообщение" },
                new Roc809EventCodes { Code = 249, Description = "Конфигурирование загрузки" },
                new Roc809EventCodes { Code = 250, Description = "Конфигурирование выгрузки" },
                new Roc809EventCodes { Code = 251, Description = "Таймаут калибровки" },
                new Roc809EventCodes { Code = 252, Description = "Отмена калибровки" },
                new Roc809EventCodes { Code = 253, Description = "Сброс мультисегментной виртуальной памяти (MVS) к заводским настройкам" }
            };

            if (!rocEventCodes.Any())
            {
                rocEventCodes.ForEach(e => { context.Roc809EventCodes.AddOrUpdate(e); });
            }            

            List<Roc809AlarmTypes> rocAlarmTypes = new List<Roc809AlarmTypes>
            {
                new Roc809AlarmTypes { Code = 0, Description = "Авария отсутствует"},
                new Roc809AlarmTypes { Code = 1, Description = "Авария параметра" },
                new Roc809AlarmTypes { Code = 2, Description = "Авария таблицы последовательности функций (FST)" },
                new Roc809AlarmTypes { Code = 3, Description = "Авария пользовательского текста" },
                new Roc809AlarmTypes { Code = 4, Description = "Авария пользовательского значения" }
            };

            if (!rocAlarmTypes.Any())
            {
                rocAlarmTypes.ForEach(a => { context.Roc809AlarmTypes.AddOrUpdate(a); });
            }            

            List<Roc809AlarmCodes> rocAlarmCodes = new List<Roc809AlarmCodes>
            {
                new Roc809AlarmCodes { Code = 0, Description = "Авария нижней предупредительной границы" },
                new Roc809AlarmCodes { Code = 1, Description = "Авария нижней аварийной границы" },
                new Roc809AlarmCodes { Code = 2, Description = "Авария верхней предупредительной границы" },
                new Roc809AlarmCodes { Code = 3, Description = "Авария верхней аварийной границы" },
                new Roc809AlarmCodes { Code = 4, Description = "Авария скорости изменения значения" },
                new Roc809AlarmCodes { Code = 5, Description = "Изменение статуса" },
                new Roc809AlarmCodes { Code = 6, Description = "Ошибка точки измерения" },
                new Roc809AlarmCodes { Code = 7, Description = "Сканирование отключено" },
                new Roc809AlarmCodes { Code = 8, Description = "Сканирование в ручном режиме" },
                new Roc809AlarmCodes { Code = 9, Description = "Переполнение суммирующих счётчиков" },
                new Roc809AlarmCodes { Code = 10, Description = "Переполнение регистра потока" },
                new Roc809AlarmCodes { Code = 11, Description = "Отсутствие потока" },
                new Roc809AlarmCodes { Code = 12, Description = "Режим 'заморозки' входов" },
                new Roc809AlarmCodes { Code = 13, Description = "Ошибка соединения с сенсором" },
                new Roc809AlarmCodes { Code = 14, Description = "Ошибка соединения интерфейса RS-485" },
                new Roc809AlarmCodes { Code = 15, Description = "Режим отключения сканирования" },
                new Roc809AlarmCodes { Code = 16, Description = "Ошибка температуры измерителя" },
                new Roc809AlarmCodes { Code = 17, Description = "Переполнение регистра потока" },
                new Roc809AlarmCodes { Code = 18, Description = "Ошибка расчёта сжимаемости" },
                new Roc809AlarmCodes { Code = 19, Description = "Сбой последовательности" },
                new Roc809AlarmCodes { Code = 20, Description = "Перекос фаз" },
                new Roc809AlarmCodes { Code = 21, Description = "Ошибка синхронизации импульсов" },
                new Roc809AlarmCodes { Code = 22, Description = "Несоответствие частот" },
                new Roc809AlarmCodes { Code = 23, Description = "Ошибка импульсного входа №1" },
                new Roc809AlarmCodes { Code = 24, Description = "Ошибка импульсного входа №2" },
                new Roc809AlarmCodes { Code = 25, Description = "Переполнение буфера импульсного выхода" },
                new Roc809AlarmCodes { Code = 26,Description = "Предупреждение о переполнении буфера импульсного выхода" },
                new Roc809AlarmCodes { Code = 27, Description = "Неисправность реле" },
                new Roc809AlarmCodes { Code = 28, Description = "Ошибка реле" },
                new Roc809AlarmCodes { Code = 29, Description = "Ограничение статического давления снизу" },
                new Roc809AlarmCodes { Code = 30, Description = "Ограничение температуры снизу" },
                new Roc809AlarmCodes { Code = 31, Description = "Ошибка обратной связи аналогового выхода" },
                new Roc809AlarmCodes { Code = 32, Description = "Плохой уровень потока импульсов" },
                new Roc809AlarmCodes { Code = 33, Description = "Авария импульса масштабной отметки" }
            };

            if (!rocAlarmCodes.Any())
            {
                rocAlarmCodes.ForEach(a => { context.Roc809AlarmCodes.AddOrUpdate(a); });
            }
        }
    }
}