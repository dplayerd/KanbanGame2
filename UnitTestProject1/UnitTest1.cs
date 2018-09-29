using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KanbanGameConsole
{
    [TestClass]
    public class HolidayTests
    {
        private TestHoliday _holiday = new TestHoliday();

        [TestMethod]
        public void today_is_Xmas()
        {
            GivenToday(12, 25);
            SayXmasShouldResponse("Merry Xmas");
        }
        [TestMethod]
        public void today_is_Xmas_12_24()
        {
            GivenToday(12, 24);
            SayXmasShouldResponse("Merry Xmas");
        }
        [TestMethod]
        public void today_is_not_Xmas()
        {
            GivenToday(11, 25);
            SayXmasShouldResponse("Today is not Xmas");
        }

        private void SayXmasShouldResponse(string expected)
        {
            Assert.AreEqual(expected, _holiday.SayXmas());
        }

        private void GivenToday(int month, int day)
        {
            _holiday.SetToday(new DateTime(2018, month, day));
        }
        [TestClass]
        internal class TestHoliday : Holiday
        {
            private DateTime _today;

            internal void SetToday(DateTime today)
            {
                _today = today;
            }

            protected override DateTime GetToday()
            {
                return this._today;
            }
        }
    }
}