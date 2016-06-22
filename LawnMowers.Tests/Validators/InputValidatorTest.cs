using Microsoft.VisualStudio.TestTools.UnitTesting;
using PC.LawnMowers.Model;
using PC.LawnMowers.Validators;

namespace LawnMowers.Tests.Validators
{
    [TestClass]
    public class InputValidatorTest
    {
        private readonly IInputValidator _validator;

        public InputValidatorTest()
        {
            _validator = new InputValidator();
        }

        #region ValidateLawnSize
        [TestMethod]
        public void ValidateLawnSize_ShouldReturnFalse()
        {
            bool validation = _validator.ValidateLawnSize("1 7 O 9");
            Assert.IsFalse(validation);
        }

        [TestMethod]
        public void ValidateLawnSize_ShouldReturnTrue()
        {
            bool validation = _validator.ValidateLawnSize("20 7");
            Assert.IsTrue(validation);
        }
        #endregion

        #region ValidateMowerPosition
        [TestMethod]
        public void ValidateMowerPosition_ShouldReturnFalseWhenFormatIsInvalid()
        {
            Lawn lawn = new Lawn(4, 7);
            bool validation = _validator.ValidateMowerPosition("S 2 45");
            Assert.IsFalse(validation);
        }

        [TestMethod]
        public void ValidateMowerPosition_ShouldReturnFalseWhenLawnWidthSmallerThanX()
        {
            bool validation = _validator.ValidateMowerPosition(new Position(83, 4, "S"), 4, 7);
            Assert.IsFalse(validation);
        }

        [TestMethod]
        public void ValidateMowerPosition_ShouldReturnFalseWhenLawnHeightSmallerThanY()
        {
            bool validation = _validator.ValidateMowerPosition(new Position(4, 83, "N"), 4, 7);
            Assert.IsFalse(validation);
        }

        [TestMethod]
        public void ValidateMowerPosition_ShouldReturnTrue()
        {
            bool validation = _validator.ValidateMowerPosition(new Position(3, 7, "E"), 4, 7);
            Assert.IsTrue(validation);
        }
        #endregion

        #region ValidateMowerCommands
        [TestMethod]
        public void ValidateMowerCommands_ShouldReturnTrue()
        {
            bool validation = _validator.ValidateMowerCommands("LMRMMLRRMMLLMMM");
            Assert.IsTrue(validation);
        }

        [TestMethod]
        public void ValidateMowerCommands_ShouldReturnFalse()
        {
            bool validation = _validator.ValidateMowerCommands("LM RMM9LRlrRMMLLMMM");
            Assert.IsFalse(validation);
        }
        #endregion
    }
}
