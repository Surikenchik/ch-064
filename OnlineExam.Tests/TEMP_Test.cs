﻿using OnlineExam.Pages.POM;
using OnlineExam.Pages.POM.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Xunit;

namespace OnlineExam.Tests
{
    [Collection("MyTestCollection")]
    public class TEMP_Test : BaseTest
    {

        private Header header;
        private SideBar TeacherTasksPage;

        public TEMP_Test(BaseFixture fixture) : base(fixture)
        {
            BeginTest();

            var header = ConstructPage<Header>();
            var logInPage = header.GoToLogInPage();
            logInPage.SignIn(Constants.TEACHER_EMAIL, Constants.TEACHER_PASSWORD);
            var TeacherTasksPage = ConstructPage<SideBar>().GoToTasksPage();
        }

        [Fact]
        public void IsTaskAvailable()
        {
            UITest(() =>
            {
                string TaskName = "Indexers";
                fixture.test = fixture.extentReports.CreateTest("IsTaskAvailable?");
                var ListOfTasks = ConstructPage<TeacherExerciseManagerPage>();
                var blocks = ListOfTasks.GetBlocks();
                if (blocks != null)
                {
                    var firstBlock = blocks.FirstOrDefault(x => x.TEMP_GetName().Equals(TaskName, StringComparison.OrdinalIgnoreCase));
                    Assert.Equal(firstBlock.TEMP_GetName(), TaskName);
                }
            }
            );
        }



        [Fact]
        public void TaskRecover()
        {
            UITest(() =>
            {
                string taskname = "Indexers";
                fixture.test = fixture.extentReports.CreateTest("TaskRecover");
                var ListOfTasks = ConstructPage<TeacherExerciseManagerPage>();
                var blocks = ListOfTasks.GetBlocks();
                if (blocks != null)
                {
                    var myblock = blocks.FirstOrDefault(x => x.Get_DELETED_TaskName().Equals(taskname, StringComparison.OrdinalIgnoreCase));
                    myblock.ClickOnRecoverButton();
                }
                else Assert.True(false);

                var NewListOfTasks = ConstructPage<TeacherExerciseManagerPage>();
                var newblocks = NewListOfTasks.GetBlocks();
                if (newblocks != null)
                {
                    var myblock = newblocks.FirstOrDefault(x => x.TEMP_GetName().Equals(taskname, StringComparison.OrdinalIgnoreCase));
                    Assert.Equal(myblock.TEMP_GetName(), taskname);
                }
                else Assert.True(false);

            }
        );
        }



        [Fact]
        public void TaskCreationDate()
        {
            UITest(() =>
            {
                string CreationDate = "23/06/2018";
                fixture.test = fixture.extentReports.CreateTest("TaskCreationDate");
                var ListOfTasks = ConstructPage<TeacherExerciseManagerPage>();

                var blocks = ListOfTasks.GetBlocks();
                if (blocks != null)
                {
                    var allblock = blocks.Where(x => x.TEMP_GetCreationDate().Equals(CreationDate, StringComparison.OrdinalIgnoreCase));
                    Assert.Equal(allblock.Count(), 3);
                }
            }
    );
        }

        [Fact]
        public void TaskUpdateDate()
        {
            UITest(() =>
            {
                string UpdateDate = "03/07/2018";
                fixture.test = fixture.extentReports.CreateTest("TaskUpdateDate");
                var ListOfTasks = ConstructPage<TeacherExerciseManagerPage>();
                var blocks = ListOfTasks.GetBlocks();
                if (blocks != null)
                {
                    var allblock = blocks.Where(x => x.TEMP_GetCreationDate().Equals(UpdateDate, StringComparison.OrdinalIgnoreCase));
                    Assert.Equal(allblock.Count(), 2);
                }
            }
);
        }


        [Fact]
        public void AddNewTaskTest()
        {
            UITest(() =>
            {
                string NEWTASK = "newtask1";
                fixture.test = fixture.extentReports.CreateTest("AddNewTaskTest");

                var TeacherTasksPage = ConstructPage<SideBar>().GoToTasksPage();
                var Tasks = ConstructPage<TeacherExerciseManagerPage>();
                Tasks.ClickOnAddTaskbutton();
                var AddTaskPage = ConstructPage<AddTaskAsTeacherPage>();
                AddTaskPage.ChooseCourse("C# Essential");
                AddTaskPage.AddTaskNameForNewTask(NEWTASK);

                                    //AddTaskPage.AddTestCasesCode("Tratataaa case code blalala");
                                    //AddTaskPage.AddDescriptionForNewTask("New description tratata blablabla");
                                    //AddTaskPage.AddBaseCodeForNewTask("yo maaaaaaaaaan");

                                    AddTaskPage.ClickOnAddButton();
                var ListOfTasks = ConstructPage<TasksPage>();
                var blocks = ListOfTasks.GetBlocks();
                if (blocks != null)
                {
                    var firstblock = blocks.FirstOrDefault(x => x.GetName().Equals(NEWTASK, StringComparison.OrdinalIgnoreCase));
                    Assert.Equal(firstblock.GetName(), NEWTASK);
                }
            }
);
        }
    }
}
