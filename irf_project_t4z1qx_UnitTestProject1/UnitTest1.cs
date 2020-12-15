using NUnit.Framework;

namespace irf_project_t4z1qx_UnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test,
            TestCase("asdasd2", false),
            TestCase("KLMOPQ", true),
            TestCase("kls3", false)
        ]
        public void TestNeptun(string nk, bool expRes)
        {
            var testhallgato = new irf_project_t4z1x.Hallgato();
            var actRes = testhallgato.ValidateHallgatoNeptunKod(nk);
            Assert.AreEqual(expRes, actRes);
        }

        [Test,
            TestCase(-5, false),
            TestCase(0.9999998, false),
            TestCase(2.6, true),
            TestCase(5.8, false)
            ]
        public void TestAtlag(double at, bool expRes)
        {
            var testhallgato = new irf_project_t4z1x.Hallgato();
            var actRes = testhallgato.ValidateHallgatoAtlag(at);
            Assert.AreEqual(expRes, actRes);
        }
        [Test,
            TestCase(12, 35, 54, false),
            TestCase(2015, 5, 8, false),
            TestCase(1997, 1, 9, true),
            TestCase(-1, -1, -1, false)
            ]
        public void TestSzulDtm(int y, int m, int d, bool expRes)
        {

            var testhallgato = new irf_project_t4z1x.Hallgato();
            var actRes = testhallgato.ValidateSzuletesiDatum(y, m, d);
            Assert.AreEqual(expRes, actRes);
        }

    }
}
