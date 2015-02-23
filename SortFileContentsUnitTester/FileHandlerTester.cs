using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SortFileContents;
using System.IO;

namespace SortFileContentsUnitTester
{
    [TestFixture]
    public class FileHandlerTester
    {

        FileHandler TestHanlder = null;
        string executionDirPath = "";
        string testUid = Guid.NewGuid().ToString();

        [SetUp]
        public void Setup()
        {
            TestHanlder = new FileHandler();
            executionDirPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            var tmpArr = executionDirPath.Split('/');
            executionDirPath = FileAddressToDriveAddress(executionDirPath.Substring(0, executionDirPath.Length - (tmpArr[tmpArr.Length - 1].Length)));
        }

        [TearDown]
        public void Disassemble()
        {
            TestHanlder = null;
        }


        [Test]
        public void ValidateNullFileParamCondition()
        {
            //check for null parameter recieved
            string errmsg = TestHanlder.SortTheFile(null);
            Assert.That(errmsg, !Is.Empty, errmsg);
        }

        [Test]
        public void ValidateEmptyParamCondition()
        {
            //check for empty parameter array recieved
            string errmsg = TestHanlder.SortTheFile(new string[] { });
            Assert.That(errmsg, !Is.Empty, errmsg);
        }


        [Test]
        public void ValidateNonExistingFileNameCondition()
        {
            //check for non Existing file name recieved condition
            string errmsg = TestHanlder.SortTheFile(new string[] { @"C:\FileDoesntExist.txt" });
            Assert.That(errmsg, !Is.Empty, errmsg);
        }

        [Test]
        public void ValidateMultiParamCondition()
        {
            //check for multiple parameter array recieved
            string errmsg = TestHanlder.SortTheFile(new string[] { @"C:\FileDoesntExist.txt", @"C:\FileDoesntExist2.txt" });
            Assert.That(errmsg, !Is.Empty, errmsg);
        }

        [Test]
        public void ValidateSimpleFileHandling()
        {
            string inputFilePath = "";
            string actualOutputFilePath = "";
            string desiredOutputFilePath = "";

            // Simple File With Everything in perfect conditon
            inputFilePath = executionDirPath + "SimpleFile_" + testUid + ".txt";
            actualOutputFilePath = executionDirPath + "SimpleFile_" + testUid + "-Sorted.txt";
            desiredOutputFilePath = executionDirPath + "SimpleFile_" + testUid + "-Desired.txt";

            File.Copy(executionDirPath + @"FilesForTesting\SimpleFileInput.txt", inputFilePath);
            File.Copy(executionDirPath + @"FilesForTesting\SimpleFileOutput.txt", desiredOutputFilePath);

            //Sorting process should not give any errors
            Assert.That(TestHanlder.SortTheFile(new string[] { inputFilePath }), Is.Empty);
            //Sorting process should generate output file
            Assert.IsTrue(File.Exists(actualOutputFilePath));
            //Validate Generated File Contents
            Assert.That(File.ReadAllText(actualOutputFilePath), Is.EqualTo(File.ReadAllText(desiredOutputFilePath)));

            File.Delete(inputFilePath);
            File.Delete(actualOutputFilePath);
            File.Delete(desiredOutputFilePath);
        }

        [Test]
        public void ValidateEmptyFileHandling()
        {
            string inputFilePath = "";
            string actualOutputFilePath = "";
            string desiredOutputFilePath = "";

            // Empty File Handling (START)
            inputFilePath = executionDirPath + "EmptyFile_" + testUid + ".txt";
            actualOutputFilePath = executionDirPath + "EmptyFile_" + testUid + "-Sorted.txt";
            desiredOutputFilePath = executionDirPath + "EmptyFile_" + testUid + "-Desired.txt";

            File.Copy(executionDirPath + @"FilesForTesting\EmptyFileInput.txt", inputFilePath);
            File.Copy(executionDirPath + @"FilesForTesting\EmptyFileOutput.txt", desiredOutputFilePath);

            //Sorting process should not give any errors
            Assert.That(TestHanlder.SortTheFile(new string[] { inputFilePath }), Is.Empty);
            //Sorting process should generate output file
            Assert.IsTrue(File.Exists(actualOutputFilePath));
            //Validate Generated File Contents (should be blank)
            Assert.That(File.ReadAllText(actualOutputFilePath), Is.EqualTo(File.ReadAllText(desiredOutputFilePath)));

            File.Delete(inputFilePath);
            File.Delete(actualOutputFilePath);
            File.Delete(desiredOutputFilePath);
        }


        [Test]
        public void ValidateFileWithEmptyLinesAndIncorrectCommas()
        {
            string inputFilePath = "";
            string actualOutputFilePath = "";
            string desiredOutputFilePath = "";

            // Complex File Handling with empty lines, multiple commas in a line & lines with comman but no first name (START)
            inputFilePath = executionDirPath + "WithSpacesAndExtraCommasOrNoCommas_" + testUid + ".txt";
            actualOutputFilePath = executionDirPath + "WithSpacesAndExtraCommasOrNoCommas_" + testUid + "-Sorted.txt";
            desiredOutputFilePath = executionDirPath + "WithSpacesAndExtraCommasOrNoCommas_" + testUid + "-Desired.txt";

            File.Copy(executionDirPath + @"FilesForTesting\WithSpacesAndExtraCommasOrNoCommasInput.txt", inputFilePath);
            File.Copy(executionDirPath + @"FilesForTesting\WithSpacesAndExtraCommasOrNoCommasOutput.txt", desiredOutputFilePath);


            //Sorting process should not give any errors
            Assert.That(TestHanlder.SortTheFile(new string[] { inputFilePath }), Is.Empty);
            //Sorting process should generate output file
            Assert.IsTrue(File.Exists(actualOutputFilePath));
            //Validate Generated File Contents 
            Assert.That(File.ReadAllText(actualOutputFilePath), Is.EqualTo(File.ReadAllText(desiredOutputFilePath)));

            File.Delete(inputFilePath);
            File.Delete(actualOutputFilePath);
            File.Delete(desiredOutputFilePath);
        }
        private string FileAddressToDriveAddress(string filename)
        {
            return filename.Replace(@"file:///", "").Replace(@"/", @"\");
        }
    }

}
