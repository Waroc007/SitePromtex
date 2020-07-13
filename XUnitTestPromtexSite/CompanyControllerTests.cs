using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PromtexSite.Controllers;
using PromtexSite.db;
using PromtexSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Moq.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace XUnitTestPromtexSite
{
    public class CompanyControllerTests
    {
        //Contact
        [Fact]
        public void ContactTest()
        {
            // Arrange
            CompanyController controller = new CompanyController();
            // Act
            ViewResult result = controller.Contact() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        //WhoAreWe
        [Fact]
        public void WhoAreWeTest()
        {
            // Arrange
            CompanyController controller = new CompanyController();
            // Act
            ViewResult result = controller.WhoAreWe() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        //Lead
        [Fact]
        public void LeadTest()
        {
            // Arrange
            CompanyController controller = new CompanyController();
            // Act
            ViewResult result = controller.Lead() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }
        //Lead1
        [Fact]
        public void Lead1Test()
        {
            // Arrange
            CompanyController controller = new CompanyController();
            // Act
            ViewResult result = controller.Lead1() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }
        //Lead2
        [Fact]
        public void Lead2Test()
        {
            // Arrange
            CompanyController controller = new CompanyController();
            // Act
            ViewResult result = controller.Lead2() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }
        //Team
        [Fact]
        public void TeamTest()
        {
            // Arrange
            CompanyController controller = new CompanyController();
            // Act
            ViewResult result = controller.Team() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        //Reviews
        [Fact]
        public void ReviewsTest()
        {
            // Arrange
            var data = new List<Review>
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
                Type= TypeReview.Catering }
            };
            var ContextMock = new Mock<ApplicationContext>(DummyOptions);
            ContextMock.Setup(x => x.Reviews).ReturnsDbSet(data);
            CompanyController controller = new CompanyController();
            // Act
            ViewResult result = controller.Reviews(ContextMock.Object as ApplicationContext) as ViewResult;
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Review>>(
                viewResult.Model);
            Assert.Equal(data.Count(), model.Count());
        }

        [Fact]
        public void NewsTest()
        {
            // Arrange
            var data = new List<News>
            {
                new News {ID =1, Name ="Алексеева Светлана Александровна", Date = DateTime.Now, Text = " ", TextMin = " "},
                new News {ID =2, Name="Ван Хаоминь", Date = DateTime.Now, Text = " ", TextMin = " "}
            };
            var ContextMock = new Mock<ApplicationContext>(DummyOptions);
            ContextMock.Setup(x => x.News).ReturnsDbSet(data);

            CompanyController controller = new CompanyController();
            // Act
            ViewResult result = controller.News(ContextMock.Object) as ViewResult;
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<News>>(
                viewResult.Model);
            Assert.Equal(data.Count(), model.Count());
        }
        [Fact]
        public void OneNewsTest()
        {
            // Arrange
            var data = new List<News>
            {
                new News {ID =1, Name ="Алексеева Светлана Александровна", Date = DateTime.Now, Text = " ", TextMin = " "},
                new News {ID =2, Name="Ван Хаоминь", Date = DateTime.Now, Text = " ", TextMin = " "}
            };
            var ContextMock = new Mock<ApplicationContext>(DummyOptions);
            ContextMock.Setup(x => x.News).ReturnsDbSet(data);

            CompanyController controller = new CompanyController();
            // Act
            ViewResult result = controller.OneNews(ContextMock.Object, 1) as ViewResult;
            NotFoundResult resultNull = controller.OneNews(ContextMock.Object, null) as NotFoundResult;
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<News>(
                viewResult.Model);
            Assert.Equal(data[0], model);
            Assert.Equal(resultNull.StatusCode, StatusCodes.Status404NotFound);
        }


        public DbContextOptions<ApplicationContext> DummyOptions { get; } = new DbContextOptionsBuilder<ApplicationContext>().Options;

    }
}
