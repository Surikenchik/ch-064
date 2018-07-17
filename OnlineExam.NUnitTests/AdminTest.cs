﻿using System;
using NUnit.Framework;
using OnlineExam.Framework;
using OnlineExam.Pages.POM;


namespace OnlineExam.NUnitTests
{
    [TestFixture]
    public class AdminTest : BaseTest
    {
        private Header header;
        private LogInPage logInPage;
        private AdminPanelPage adminPanelPage;


        [SetUp]
        public void SetUp()
        {
            BeginTest();

            header = ConstructPage<Header>();
            logInPage = header.GoToLogInPage();
            logInPage.SignIn(Constants.ADMIN_EMAIL, Constants.ADMIN_PASSWORD);
            adminPanelPage = ConstructPage<SideBar>().GoToAdminPanelPage();
        } 
        
   
        [Test]
        public void IsUserPresentedInUserListTest()
        {
            UITest(() =>
            {
                Assert.True(adminPanelPage.IsUserPresentedInUserList(Constants.STUDENT_EMAIL));
            });
        }

        [Test]
        public void DeleteUserTest()
        {
            UITest(() =>
            {
                Assert.True(adminPanelPage.IsUserPresentedInUserList(Constants.USER_FOR_DELETE_EMAIL),
                    "User is not presented in the system," +
                    "so we have not opportunity to delete this user");
                adminPanelPage.DeleteUser(Constants.USER_FOR_DELETE_EMAIL);

                Assert.True(adminPanelPage.IsListOfUsersH2ElementPresented());
                Assert.False(adminPanelPage.IsUserPresentedInUserList(Constants.USER_FOR_DELETE_EMAIL), "Error");

            });
        }

        [Test]
        public void ChangeUserRoleTest()
        {
            UITest(() =>
            {
                var changeRolePage = adminPanelPage.ChangeRoleOfUserButtonClick(Constants.USER_FOR_CHANGE_ROLE_EMAIL);
                changeRolePage.ChangeRole(Constants.TEACHER);
                changeRolePage = adminPanelPage.ChangeRoleOfUserButtonClick(Constants.USER_FOR_CHANGE_ROLE_EMAIL);
                Assert.AreEqual(Constants.TEACHER, changeRolePage.CurrentRole());
            });
        }

        [Test]
        public void IsUserListAvailableTest()
        {
            UITest(() =>
            {
                Assert.True(adminPanelPage.IsListOfUsersH2ElementPresented());
            });
        }
        }
}