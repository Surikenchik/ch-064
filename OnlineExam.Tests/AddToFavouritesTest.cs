﻿using OnlineExam.Pages.POM;
using OnlineExam.Pages.POM.CodeHistory.Favourites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineExam.Tests
{
    public class AddToFavouritesTest : BaseTest
    {
        public AddToFavouritesTest()
        {
        }

        [Fact]
        public void TestAddToFovourites()
        {
            BeginTest();
            driver.Navigate().GoToUrl("http://localhost:55842/Account/Login");
            System.Threading.Thread.Sleep(1000);
            var loginPage = new LogInPage(driver);
            var indexPage = loginPage.SignIn("student3@gmail.com", Constants.STUDENT_PASSWORD);

            var goToHistoryPage = new SideBar(driver).GoToCodeHistoryPage();
            var historyPage = new CodeHistoryPage(driver);
            var blocks = historyPage.GetBlocks();
            if (blocks.Any())
            {
                var firstBlock = blocks[1];
                firstBlock.ClickOnTitle();
                firstBlock.IsLiked();
            }
            //likeCode.SwitchToFavourites();
            //var newBlock = new HistoryFavouriteBlock(driver);
            ////var executedCode = newBlock.CodeHistoryBlockOfExecutedCode[0]
            //string colorOfLikeButton = newBlock.LikeButton.GetCssValue("color");


            //if (colorOfLikeButton == "rgba(51, 51, 51, 1)")
            //{
            //    var likeCode = new CodeHistoryPage(driver);
            //    likeCode.SwitchToFavourites();
            //}
            //var isEqual = string.Equals(colorOfLikeButton, "rgba(255, 0, 0, 1)");
            //Assert.True(isEqual);

            //var historyPage = new CodeHistoryPage(driver);
            //historyPage.SwitchToFavourites();

            //var newBlockFavourites = new HistoryFavouriteBlock(driver);
            //var isEqualInFovourites = string.Equals(newBlock.IndexersFieldText, newBlockFavourites.IndexersFieldText);

            //Assert.True(isEqualInFovourites);



        }
    }
}