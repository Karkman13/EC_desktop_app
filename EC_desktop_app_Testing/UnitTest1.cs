using EC_desktop_app.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Moq;
using System.Text;

namespace EC_desktop_app_Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void OpenFileDialog_true()
        {
            // arrange
            var expected = true;
            var expectedFileName = "5555.xml";

            var fileSystem = new Mock<IFileSystem>();
            fileSystem.Setup(_ => _.ReadAllText(expectedFileName, It.IsAny<Encoding>())).Returns(expectedFileName).Verifiable();

            var defaultFileIDialog = new Mock<IDialogService>();
            defaultFileIDialog.Setup(_ => _.OpenFileDialog()).Returns(true).Verifiable();
            defaultFileIDialog.Setup(_ => _.FilePath).Returns(expectedFileName).Verifiable();

            // act
            var defaultDialogService = new DefaultDialogService();
            defaultDialogService.FilePath = "C:\\Users\\karkm\\Downloads\\5555.xml";
            var actual = defaultDialogService.OpenFileDialog();

            // assert
            fileSystem.Verify();
            defaultFileIDialog.Verify();

            Assert.AreEqual(expected, actual);
        }
    }
}
