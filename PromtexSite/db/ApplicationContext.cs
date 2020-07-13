using Microsoft.EntityFrameworkCore;
using PromtexSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromtexSite.db
{
   
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<TablePrice> Prices { get; set; }
        public virtual DbSet<Medal> Medals { get; set; }
        public virtual DbSet<Question> Questions { get; set; }

        public ApplicationContext(DbContextOptions opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasData(
                new Review[]
                {
                new Review {ID =1, Counter = 1, Name ="Алексеева Светлана Александровна", CompanyName="ООО \"Офис\"", Text=@"ООО «Офис» порядка десятка лет занимается поставкой жалюзи и оказанием сопутствующих услуг корпоративным клиентам в Санкт-Петербурге. Светлана Алексеева руководитель компании пишет:

«Мой бизнес не является для меня основной деятельностью. Он для меня, как хобби или, скорее, приработок. При такой ситуации, естественно, что я не могу уделять ему много внимания, особенно в рабочее время.

Поэтому выражаю благодарность компании «1С:БухОбслуживание. ПрофБО» за организованный сервис. Для меня это, что называется: «все включено». Тарифный план «Комплексный сервис» плюс дополнительные услуги, оказываемые по моему поручению, такие как «оплата счетов», «удаленный секретарь», услуги курьера и некоторые другие, позволяют мне не заботиться об оформлении и обмене документами с контрагентами и, собственно, бухгалтерском и налоговом учете, сдаче регламентированной отчетности.

Я думаю, что даже не полная ответственность за ущерб, закрепленная в договоре, сколько простая формула, «чем лучше идут дела в моем бизнесе, тем больше хозяйственных операций и сумма за обслуживание», позволила мне найти единомышленников по части взаимодействия с моими заказчиками. Уже несколько раз слышала слова благодарности за четкую работу и вежливое обращение. При этом у меня нет никакой заботы, контролировать кого-либо или организовывать. Страшно вспоминать, сколько я потратила ранее в пустую времени, сил и денег на штатного офис-менеджера, бухгалтера и курьера. Ведь нельзя же оставить кого-то одного, не будет резерва. А вдруг кто-то заболеет или уволиться. Так они хоть худо-бедно могли подменить друг друга на время в какой-то части.

Теперь сумма моих затрат по этим участкам работы не превышает заработной платы одного достойного специалиста. Ну, иногда, в удачные месяцы - заработной платы специалиста плюс начисления на нее. А в неудачные месяцы, не сезон я просто плачу меньше и не рискую влезть в убыток, оправляться от которого приходилось несколько месяцев подряд. И, неизвестно, оправишься ли вообще.

Признаться, я даже не ожидала, что такое возможно! Очень довольна!»", CompanyPosition="Генеральный директор",
                    TextMin = "Для меня сервис «1С:БухОбслуживание. ПрофБО», что называется: «все включено».",
                    PhotoFolder = @"/img/Review/alekseeva_small.jpg",
                    ReviewFolder =@"/img/Review/ofis.jpg",
                    Type = TypeReview.Production

                },
                new Review {ID =2, Counter = 2, Name="Ван Хаоминь", CompanyName="ООО\"Юй Синь\"", Text=@"Хочу выразить благодарность за качественно оказываемые услуги по ведению бухгалтерии.

Сотрудничаем с «1С:БухОбслуживание.ПрофБО» (ООО «Промтекс») на протяжении полутора лет. За это время мы успели оценить по достоинству квалификацию бухгалтеров, обслуживающих нашу компанию, и результаты их работы.

С большим удовольствием рекомендую услуги «1С:БухОбслуживание.ПрофБО» (ООО «Промтекс») небольшим компаниям, индивидуальным предпринимателям и всем руководителям, которые желают переложить обязанность по ведению учета на ответственного и профессионального исполнителя.",
                TextMin ="Хочу выразить благодарность за качественно оказываемые услуги по ведению бухгалтерии.", CompanyPosition="Генеральный директор",
                PhotoFolder=@"/img/Review/yuisin_small.jpg",
                ReviewFolder =@"/img/Review/yuisin_preview.jpg",
                Type= TypeReview.Catering
                }
                });
            modelBuilder.Entity<TablePrice>().HasData(
                new TablePrice[]
                {
                    new TablePrice { ID = 1, Type = TableType.RecordKeeping, Name = "Составление типовой учетной политики", Price = 1200, Unit = "документ"},
                    new TablePrice { ID = 2, Type = TableType.RecordKeeping, Name = "Разработка индивидуальной учетной политики", Price =1200  , Unit = "час"},
                    new TablePrice { ID = 3, Type = TableType.RecordKeeping, Name = "Разработка индивидуальной учетной политики", Price = 1200, Unit = "час"},
                    new TablePrice { ID = 4, Type = TableType.RecordKeeping, Name = "Разработка индивидуальной учетной политики", Price =1200 , Unit = "час"},

                    new TablePrice { ID = 5, Type = TableType.PersonnelAccounting, Name = "Составление штатного расписания", Price =2100 , Unit = "час"},
                    new TablePrice { ID = 6, Type = TableType.PersonnelAccounting, Name = "Предоставление образцов заявлений работника (о приеме на работу, об увольнении, о предоставлении отпуска и т.п.)", Price =200 , Unit = "документ"},
                    new TablePrice { ID = 7, Type = TableType.PersonnelAccounting, Name = "Предоставление образцов записей в трудовую книжку", Price =200     , Unit = "документ"},
                    new TablePrice { ID = 8, Type = TableType.PersonnelAccounting, Name = "Разработка/изменение Положения об оплате труда (премировании)", Price =2100 , Unit = "час"},
                    new TablePrice { ID = 9, Type = TableType.PersonnelAccounting, Name = "Разработка/изменение Правил внутреннего трудового распорядка", Price =2100 , Unit = "час"},
                    new TablePrice { ID = 10, Type = TableType.PersonnelAccounting, Name = "Оформление графика отпусков", Price =2100 , Unit = "час"},
                    new TablePrice { ID = 11, Type = TableType.PersonnelAccounting, Name = "Разработка/изменение Положения о защите персональных данных", Price =2100 , Unit = "час"},
                    new TablePrice { ID = 12, Type = TableType.PersonnelAccounting, Name = "Разработка/изменение Положения о командировках", Price =2100 , Unit = "час"},
                    new TablePrice { ID = 13, Type = TableType.PersonnelAccounting, Name = "Восстановление кадрового учета", Price =2100 , Unit = "час"},
                    new TablePrice { ID = 14, Type = TableType.PersonnelAccounting, Name = "Заполнение оригинала больничного листа вручную/за 1 документ", Price =450 , Unit = "больничный лист"},
                    new TablePrice { ID = 15, Type = TableType.PersonnelAccounting, Name = "Оформление пакета документов в командировку (Форма Т-9, Т-10, Т-10а)", Price =350 , Unit = "пакет документов"},

                    new TablePrice { ID = 16, Type = TableType.PayrollPreparation, Name = "Обучение сотрудников Заказчика работе в 1С ЗУП", Price =2100 , Unit = "час"},
                    new TablePrice { ID = 17, Type = TableType.PayrollPreparation, Name = "Восстановление расчета заработной платы за предыдущие периоды", Price =2100 , Unit = "час"},
                    new TablePrice { ID = 18, Type = TableType.PayrollPreparation, Name = "Подготовка и сдача комплекта документов в ФСС на возмещение выплат по больничным листам и т.п.", Price =2100 , Unit = "час"},

                    new TablePrice { ID = 19, Type = TableType.ReportingAndSubmission, Name = "Сдача отчетности курьером Исполнителя", Price =1000 , Unit = "комплект отчетности"},
                    new TablePrice { ID = 20, Type = TableType.ReportingAndSubmission, Name = "Сдача отчетности почтовым отправлением с описью вложения", Price =1000 , Unit = "комплект отчетности"},
                    new TablePrice { ID = 21, Type = TableType.ReportingAndSubmission, Name = "Формирование квартальной «Нулевой отчетности» ОСН", Price =3976 , Unit = "комплект отчетности"},
                    new TablePrice { ID = 22, Type = TableType.ReportingAndSubmission, Name = "Формирование квартальной «Нулевой отчетности» УСН/ ЕСХН", Price =3300 , Unit = "комплект отчетности"},
                    new TablePrice { ID = 23, Type = TableType.ReportingAndSubmission, Name = "Формирование \"алкогольной\" декларации", Price =2100 , Unit = "час"},
                    new TablePrice { ID = 24, Type = TableType.ReportingAndSubmission, Name = "Формирование декларации 3-НДФЛ", Price =2100 , Unit = "час"},

                    new TablePrice { ID = 25, Type = TableType.StorageServices, Name = "Хранение архива документов (для клиентов регулярного сервиса \"1С:БухОбслуживание\" только для документов прошедшего отчетного года после получения уведомления от Исполнителя о готовности их к передаче)", Price =50 , Unit = "архивное дело/месяц"},

                    new TablePrice { ID = 26, Type = TableType.AccountingAdvice, Name = "Письменное налоговое и бухгалтерское консультирование", Price =2100 , Unit = "час"},
                    new TablePrice { ID = 27, Type = TableType.AccountingAdvice, Name = "Финансовое и налоговое планирование", Price =2100 , Unit = "час"},

                    new TablePrice { ID = 28, Type = TableType.SecretaryServices, Name = "Изготовление ксерокопии/сканкопии документов за лист", Price =50 , Unit = "лист А4"},
                    new TablePrice { ID = 29, Type = TableType.SecretaryServices, Name = "Составление реестра документов для их приема-передачи/за каждый документ (строку) в реестре", Price =5 , Unit = "документ"},
                    new TablePrice { ID = 30, Type = TableType.SecretaryServices, Name = "Выезд курьера в Ленинградскую область", Price =25 , Unit = "км"},
                    new TablePrice { ID = 31, Type = TableType.SecretaryServices, Name = "Выезд курьера по СПб", Price =500 , Unit = "поездка"},
                    new TablePrice { ID = 32, Type = TableType.SecretaryServices, Name = "«Удаленный секретарь»: составление деловых писем, документов и т.п.", Price =2100 , Unit = "час"},

                    new TablePrice { ID = 33, Type = TableType.LegalServices, Name = "Получение выписки из ЕГРЮЛ/ЕГРИП в обычном режиме 5-7 рабочих дней", Price =1200 , Unit = "документ"},
                    new TablePrice { ID = 34, Type = TableType.LegalServices, Name = "Получение выписки из ЕГРЮЛ/ЕГРИП в срочном режиме 1-2 рабочих дня", Price =1600 , Unit = "документ"},
                    new TablePrice { ID = 35, Type = TableType.LegalServices, Name = "Составление заявления о постановке на учет в качестве плательщика ЕНВД, патента", Price =1000 , Unit = "документ"},
                    new TablePrice { ID = 36, Type = TableType.LegalServices, Name = "Составление заявления о переходе на УСН или отказ от применения УСН", Price =1000 , Unit = "документ"},
                    new TablePrice { ID = 37, Type = TableType.LegalServices, Name = "Составление заявления о смене объекта налогообложения при УСН", Price =1000 , Unit = "документ"},
                    new TablePrice { ID = 38, Type = TableType.LegalServices, Name = "Регистрация (ликвидация) обособленного подразделения в налоговом органе по месту нахождения в СПб", Price =3000 , Unit = "шт."},

                    new TablePrice { ID = 39, Type = TableType.ComprehensiveService, Name = "ЕНВД", Price =3110 , Unit = "от 3110 рублей/месяц"},
                    new TablePrice { ID = 40, Type = TableType.ComprehensiveService, Name = "УСНО «доходы»", Price =1780 , Unit = "от 1780 рублей/месяц"},
                    new TablePrice { ID = 41, Type = TableType.ComprehensiveService, Name = "УСНО «доходы-расходы» или ЕСХН", Price =1780 , Unit = "от 1780 рублей/месяц"},
                    new TablePrice { ID = 42, Type = TableType.ComprehensiveService, Name = "ОСНО", Price =6220 , Unit = "от 6220 рублей/месяц"},
                    new TablePrice { ID = 43, Type = TableType.ComprehensiveService, Name = "Комбинированные СНО", Price =5600 , Unit = "от 5600 рублей/месяц"}
                }
                );
            modelBuilder.Entity<Medal>().HasData(
                new Medal[]
                {
                    new Medal {ID=1, Text="Отличник технологии", Visible=true },
                    new Medal {ID=2, Text="Команда профессиональных бухгалтеров", Visible=true },
                    new Medal {ID=3, Text="Регистрация бизнеса", Visible=true },
                    new Medal {ID=4, Text="Более 20 клиентов в сфере «Услуг»", Visible=true },
                    new Medal {ID=5, Text="Отличник продаж", Visible=true },
                    new Medal {ID=6, Text="Более 20 клиентов в сфере «Оптовая торговля»", Visible=true },
                    new Medal {ID=7, Text="Более 20 клиентов в сфере «Розничная торговля»", Visible=true },
                    new Medal {ID=8, Text="Лидер отзывов клиентов", Visible=false },
                    new Medal {ID=9, Text="Нам доверяют более 100 клиентов", Visible=false },
                    new Medal {ID=10, Text="Более 20 клиентов в сфере «Строительство»", Visible=false },
                    new Medal {ID=11, Text="Страхование финансовой ответственности", Visible=false },
                    new Medal {ID=12, Text="Более 20 клиентов в сфере «Производство»", Visible=false },
                });
        }
    }
}
