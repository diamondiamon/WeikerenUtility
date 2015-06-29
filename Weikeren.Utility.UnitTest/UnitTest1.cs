using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;

namespace Weikeren.Utility.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dt = DateTime.MinValue;
        }
        #region TestTask

        [TestMethod]
        public void TestTask()
        {
            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(3000);
            //    System.Diagnostics.Debug.WriteLine("After Three Second");
            //});
            //System.Diagnostics.Debug.WriteLine("Has Output");



            // use an Action delegate and a named method
            Task task1 = new Task(obj=>printMessage((string)obj),"1");
            // use a anonymous delegate
            Task task2 = new Task(delegate
            {
                printMessage("2");
            });

            // use a lambda expression and a named method
            Task task3 = new Task(() => printMessage("3"));
            // use a lambda expression and an anonymous method
            Task task4 = new Task(() =>
            {
                printMessage("4");
            });

            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();

            System.Diagnostics.Debug.WriteLine("Done");
        }

        private void printMessage(string index)
        {
            System.Diagnostics.Debug.WriteLine("Hello World ：" + index);
        }


        [TestMethod]
        public void TestCancelTask()
        {
            // create the cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            // create the cancellation token
            CancellationToken token = tokenSource.Token;
            // create the task

            bool flag = true;

            Task task = new Task(() =>
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    //if (token.IsCancellationRequested)
                    //{
                    //    System.Diagnostics.Debug.WriteLine("Task cancel detected");
                        
                    //}
                    //else
                    //{
                    //    System.Diagnostics.Debug.WriteLine("Int value {0}", i);
                    //}
                    if (flag)
                    {
                        System.Diagnostics.Debug.WriteLine("Int value {0}", i);
                    }
                    else
                    {
                        break;
                    }
                }
            }, token);

            // register a cancellation delegate
            token.Register(() =>
            {
                System.Diagnostics.Debug.WriteLine(">>>>>> Delegate Invoked\n");
                flag = false;
            });


            // wait for input before we start the task
            System.Diagnostics.Debug.WriteLine("Press enter to start task");
            System.Diagnostics.Debug.WriteLine("Press enter again to cancel task");

            Thread.Sleep(1000);

            // start the task
            task.Start();

            // read a line from the console.
            Thread.Sleep(1000);

            // cancel the task
            Console.WriteLine("Cancelling task");
            tokenSource.Cancel();

            // wait for input before exiting
            System.Diagnostics.Debug.WriteLine("Main method complete. Press enter to finish.");
            Thread.Sleep(100000);
        }

        [TestMethod]
        public void TestWait()
        {
            // create the cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            // create the cancellation token
            CancellationToken token = tokenSource.Token;

            // create the first task, which we will let run fully
            Task task1 = new Task(() =>
            {
                for (int i = 0; i < Int32.MaxValue; i++)
                {
                    // put the task to sleep for 10 seconds
                    bool cancelled = token.WaitHandle.WaitOne(1000);
                    // print out a message
                    System.Diagnostics.Debug.WriteLine("Task 1 - Int value {0}. Cancelled? {1}",
                    i, cancelled);
                    // check to see if we have been cancelled
                    if (cancelled)
                    {
                        //throw new OperationCanceledException(token);
                        break;
                    }
                }
            }, token);
            // start task
            task1.Start();

            // wait for input before exiting
            System.Diagnostics.Debug.WriteLine("Press enter to cancel token.");
            Thread.Sleep(3000);

            // cancel the token
            tokenSource.Cancel();

            // wait for input before exiting
            System.Diagnostics.Debug.WriteLine("Main method complete. Press enter to finish.");
            Thread.Sleep(20000);
        }

        [TestMethod]
        public void TestTS()
        {
            var ts = new TimeSpan(0);
            var k = ts.TotalMilliseconds;
        }

        #endregion


        [TestMethod]
        public void TestAssemblyInfo()
        {
            var type = this.GetType();
            var assemblyInfo = type.Assembly.FullName;
        }

    }
}
