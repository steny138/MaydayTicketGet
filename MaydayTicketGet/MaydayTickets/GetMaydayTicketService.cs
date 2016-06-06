using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace MaydayTicketGet.MaydayTickets
{
    public class GetMaydayTicketService
    {
        private  ChromeDriver _driver  = null;
        private TicketSetting _setting = null;
        private string _startUrl = string.Empty;

        public static string MAYDAY_TICKEY_NOMAL_URL = "http://tixcraft.com/activity/detail/2016_JRI";
        public static string MAYDAY_TICKEY_3800_URL = "http://tixcraft.com/activity/detail/2016_JRI2";
        public static string MAYDAY_TICKEY_TEST_URL = "http://tixcraft.com/activity/detail/16_SLIPPA1";

        public GetMaydayTicketService(string starturl, TicketSetting setting) {
            this._startUrl = starturl;
            this._setting = setting;
        }

        public string start()
        {
            StringBuilder result = new StringBuilder();
            ChromeDriverService ser = ChromeDriverService.CreateDefaultService("drivers");
            ser.HideCommandPromptWindow = true;
            _driver = new ChromeDriver(ser, new ChromeOptions());

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            _driver.Url = _startUrl;
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            //Step.1 Login
            _driver.FindElementById("loginFacebook").Click();
            _driver.FindElementById("email").SendKeys(this._setting.facebook.email);
            _driver.FindElementById("pass").SendKeys(this._setting.facebook.password);
            _driver.FindElementById("loginbutton").Click();
            
            //Step.2 Wait
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            
            foreach (Ticket ticket in this._setting.tickets)
            {
                try
                {
                    _driver.Url = _startUrl;
                    _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

                    _driver.FindElementByPartialLinkText("立即購票").Click();
                    //#content > div > div:nth-child(2) > div:nth-child(3) > ul > li:nth-child(1) > a
                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("dateSearchGameList")));
                    _driver.FindElementById("dateSearchGameList").SendKeys(ticket.date);

                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(
                        By.CssSelector("tbody > tr:nth-child(1) > td:nth-child(4) > input[type=\"button\"]")));

                    _driver.FindElementById("gameList")
                        .FindElement(By.CssSelector("tbody > tr:nth-child(1) > td:nth-child(4) > input[type=\"button\"]")).Click();

                    //Step.4 choose seat 
                    _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

                    var areas = _driver.FindElementsByClassName("area-list");
                    IWebElement matchSeat = findMatchSeat(areas, ticket.seat);

                    if (matchSeat != null)
                    {
                        matchSeat.Click();
                    }
                    else
                    {
                        continue;
                    }

                    //Step.4 check ticket ready send!
                    _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

                    _driver.FindElementByCssSelector("#ticketPriceList >tbody select[name*=TicketForm]").SendKeys(ticket.num.ToString());

                    _driver.FindElementById("ticketPriceSubmit").Click();

                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("submitButton")));

                    //Step.5 Payment
                    IWebElement matchPayment = findMatchPayment(_driver.FindElementsByCssSelector("#PaymentForm label"), "信用卡");
                    if (matchPayment != null)
                    {
                        matchPayment.Click();
                    }
                    else
                    {
                        continue;
                    }

                    //submitButton
                    _driver.FindElementById("cancelOrder").Click();


                    _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));


                    if (_driver.Url == " http://tixcraft.com/order")
                    {
                        break;
                    }
                }
                catch(Exception ex) {
                    result.AppendLine(ex.ToString());
                }
            }
            return result.ToString();
        }

        private IWebElement findMatchSeat(System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> areas, string seatName)
        {
            if (areas != null && areas.Count > 0)
            {
                foreach (var area in areas)
                {
                    //find availible seat 
                    var seats = area.FindElements(By.CssSelector("li>a"));
                    foreach (var seat in seats)
                    {
                        if (seat.Text.Contains(seatName))
                        {
                            return seat;
                        }
                    }
                }
            }
            return null;
        }

        private IWebElement findMatchPayment(System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> payments, string paymentMethod)
        {
            if (payments != null && payments.Count > 0)
            {
                foreach (IWebElement payment in payments)
                {
                    //find availible seat 
                    if (payment.Text.Contains(paymentMethod))
                    {
                        return payment.FindElement(By.CssSelector("input"));
                    }
                }
            }
            return null;
        }

        public void end()
        {
            _driver.Dispose();
        }
       
    }
}
