using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillingAdmin.Models
{
    [MetadataType(typeof(Services_Preferences_HB_Model))]
    public partial class Services_Preferences_HB
    {
        public class Services_Preferences_HB_Model
        {
            [DisplayName("Свойство")]
            public string name { get; set; }

            [DisplayName("Тип свойства")]
            public int typeId { get; set; }
        }

    }

    [MetadataType(typeof(Services_Preferences_Types_Model))]
    public partial class Services_Preferences_Types
    {
        public class Services_Preferences_Types_Model
        {
            [DisplayName("Тип")]
            public string name { get; set; }
        }

    }

    [MetadataType(typeof(Services__Model))]
    public partial class Services
    {
        public class Services__Model
        {
            [DisplayName("Название услуги")]
            public string name { get; set; }


            [DisplayName("Описание услуги")]
            public string description { get; set; }
        }

    }

    [MetadataType(typeof(Services_Preferences__Model))]
    public partial class Services_Preferences
    {
        public class Services_Preferences__Model
        {
            [DisplayName("Название услуги")]
            public int serviceid { get; set; }

            [DisplayName("Свойство услуги")]
            public int preferenceId { get; set; }
        }

    }

    [MetadataType(typeof(Services_Prices_Model))]
    public partial class Services_Prices
    {
        public class Services_Prices_Model
        {
            [DisplayName("Услуга")]
            public int serviceId { get; set; }

            [DisplayName("Вид расчета")]
            public int priceTypesId { get; set; }

            [DisplayName("Тариф")]
            public Nullable<int> tarifId { get; set; }

            [DisplayName("Стоимость")]
            public Nullable<int> value { get; set; }

            [DisplayName("Дата создания")]
            public Nullable<System.DateTime> date_create { get; set; }

            [DisplayName("Дата начала")]
            public Nullable<System.DateTime> date_start { get; set; }

            [DisplayName("Дата окончания")]
            public Nullable<System.DateTime> date_stop { get; set; }

            [DisplayName("Активно")]
            public Nullable<bool> isActive { get; set; }

            [DisplayName("Тарифицируемый параметр")]
            public Nullable<int> preferenceId { get; set; }


            public virtual Price_TypesHB Price_TypesHB { get; set; }
            public virtual Services Services { get; set; }
            public virtual Tarifs Tarifs { get; set; }
            public virtual Services_Preferences_HB Services_Preferences_HB { get; set; }
        }

    }


    [MetadataType(typeof(Price_TypesHB_Model))]
    public partial class Price_TypesHB
    {
        public class Price_TypesHB_Model
        {
            [DisplayName("Вид расчета")]
            public string name { get; set; }
        }
    }

    [MetadataType(typeof(Statistic_pbx_Model))]
    public partial class Statistic_pbx
    {
        public class Statistic_pbx_Model
        {

            [DisplayName("N")]
            public int id { get; set; }

            [DisplayName("Идентификатор")]
            public string UNIQUEID { get; set; }

            [DisplayName("Начало звонка")]
            public string start { get; set; }

            [DisplayName("Время ответа")]
            public string answer { get; set; }

            [DisplayName("Окончание звонка")]
            public string endtime { get; set; }

            [DisplayName("Код источника")]
            public string src_chan { get; set; }

            [DisplayName("Номер звонящего")]
            public string src_num { get; set; }

            [DisplayName("Код приемника")]
            public string dst_chan { get; set; }

            [DisplayName("Номер принявшего")]
            public string dst_num { get; set; }

            [DisplayName("Идентификатор линии")]
            public string linkedid { get; set; }

            [DisplayName("Код проекта")]
            public string did { get; set; }

            [DisplayName("Дополнительно")]
            public string disposition { get; set; }

            [DisplayName("Запись звонка")]
            public string recordingfile { get; set; }

            [DisplayName("Аккаунт источника")]
            public string from_account { get; set; }

            [DisplayName("Аккаунт принявшего")]
            public string to_account { get; set; }

            [DisplayName("Статус")]
            public string dialstatus { get; set; }

            [DisplayName("Приожение")]
            public string appname { get; set; }

            [DisplayName("Переадресация")]
            public string transfer { get; set; }

            [DisplayName("Приложение")]
            public string is_app { get; set; }

            [DisplayName("Длительность")]
            public string duration { get; set; }

            [DisplayName("В разговоре")]
            public string billsec { get; set; }

            [DisplayName("Код завершения")]
            public string work_completed { get; set; }
        }
    }


    [MetadataType(typeof(Accounts_Model))]
    public partial class Accounts
    {

        public class Accounts_Model
        {
            public int Id { get; set; }

            #region Общие данные

            [DisplayName("Сокращенное название")]
            [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
            public string ShortLegalName { get; set; }

            [DisplayName("Полное название")]
            [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
            public string FullLegalName { get; set; }

            [DisplayName("ОГРН")]
            [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
            public string Ogrn { get; set; }

            [DisplayName("ИНН")]
            [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
            public string INN { get; set; }

            [DisplayName("КПП")]
            [StringLength(9, ErrorMessage = "{0} должен быть {1} знаков", MinimumLength = 9)]
            public string KPP { get; set; }

            [DisplayName("Фактический адрес")]
            public string Actual_Street { get; set; }

            [DisplayName("Юридический адрес")]
            public string Legal_Street { get; set; }

            [DisplayName("Адрес сайта")]
            [Url(ErrorMessage = "{0} не содержит допустимый полный URL-адрес http, https или ftp.")]
            public string Site { get; set; }

            #endregion


            #region Контактные данные

            [DisplayName("Директор (ФИО)")]
            [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
            public string DirectorFullName { get; set; }

            [DisplayName("Должность директора")]
            public string PostDirector { get; set; }

            [DisplayName("Документ подтверждающий полномочия")]
            public string Document { get; set; }

            [DisplayName("Контактное лицо")]
            public string ContactName { get; set; }

            [DisplayName("Телефон")]
            [Phone]
            public string Phone { get; set; }

            [DisplayName("Email адрес")]
            [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
            [EmailAddress(ErrorMessage = "{0} не содержит допустимый адрес электронной почты")]
            public string Email { get; set; }

            [DisplayName("Вопросы взаимодействия")]
            public string Interaction { get; set; }

            #endregion



            #region Банковские реквизиты

            [DisplayName("БИК")]
            [StringLength(9, ErrorMessage = "{0} должен быть {1} знаков", MinimumLength = 9)]
            public string Bank_Bic { get; set; }

            [DisplayName("Наименование банка")]
            public string Bank_Name { get; set; }

            [DisplayName("Расчетный счет")]
            [StringLength(20, ErrorMessage = "{0} должен быть {1} знаков", MinimumLength = 20)]
            public string Bank_Account { get; set; }

            [DisplayName("Корреспонденский счет")]
            [StringLength(20, ErrorMessage = "{0} должен быть {1} знаков", MinimumLength = 20)]
            public string Bank_Korrespondent { get; set; }

            #endregion


            #region Договор

            [DisplayName("Номер договора")]
            public string NumberContract { get; set; }

            [DisplayName("Дата регистрации договора")]
            public DateTime? DateContract { get; set; }

            #endregion
        }

    }
}