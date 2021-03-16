using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class PlayerCharacterShould : IDisposable
    {
        private readonly PlayerCharacter _sut;
        private readonly ITestOutputHelper _output;

        public PlayerCharacterShould(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Creating new PlayerCharacter");
            _sut = new PlayerCharacter();
        }

        public void Dispose()
        {
            _output.WriteLine($"Disposing PlayerCharacter {_sut.FullName}");

            //_sut.Dispose();
        }

        [Fact]
        public void BeInexperiencedWhenNew()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            Assert.True(_sut.IsNoob);
        }

        [Fact]
        public void CalculateFullName()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            Assert.Equal("Sarah Smith", _sut.FullName);
        }

        [Fact]
        public void HaveFullNameStartingWithFirstName()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            Assert.StartsWith("Sarah", _sut.FullName);
        }

        [Fact]
        public void HaveFullNameEndingWithLastName()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            Assert.EndsWith("Smith", _sut.FullName);
        }

        [Fact]
        public void CalculateFullName_IgnoreCaseAssertExample()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            _sut.FirstName = "SARAH";
            _sut.LastName = "SMITH";

            Assert.Equal("Sarah Smith", _sut.FullName, ignoreCase: true);
        }

        [Fact]
        public void CalculateFullName_SubstringAssertExample()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            Assert.Contains("ah Sm", _sut.FullName);
        }

        [Fact]
        public void CalculateFullName_WithTitleCase()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", _sut.FullName);
        }

        [Fact]
        public void StartWithDefaultHealth()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            Assert.Equal(100, _sut.Health);
        }

        [Fact]
        public void StartWithDefaultHealth_NotEqualExample()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            Assert.NotEqual(0, _sut.Health);
        }

        [Fact]
        public void IncreaseHealthAfterSleeping()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            _sut.Sleep(); //Expect increase between 1 to 100 inclusive

            //Assert.True(sut.Health >= 100 && sut.Health <= 200);
            Assert.InRange(_sut.Health, 101, 200);
        }

        [Fact]
        public void NotHaveNickNameByDefault()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            Assert.Null(_sut.Nickname);
        }

        [Fact]
        public void HaveALongBow()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            Assert.Contains("Long Bow", _sut.Weapons);
        }

        [Fact]
        public void ShouldNotHaveStaffOfWonder()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            Assert.DoesNotContain("Staff Of Wonder", _sut.Weapons);
        }

        [Fact]
        public void HaveAtLeastOneKindOfSword()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            Assert.Contains(_sut.Weapons, weapon => weapon.Contains("Sword"));
        }

        [Fact]
        public void HaveAllExpectedWeapons()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            var expectedWeapons = new[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            Assert.Equal(expectedWeapons, _sut.Weapons);
        }

        [Fact]
        public void HaveNoEmptyWeapons()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            Assert.All(_sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
        }

        [Fact]
        public void RaiseSleepEvent()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            Assert.Raises<EventArgs>(
                handler => _sut.PlayerSlept += handler,
                handler => _sut.PlayerSlept -= handler,
                () => _sut.Sleep());
        }

        [Fact]
        public void RaisePropertyChangeEvent()
        {
            //PlayerCharacter sut = new PlayerCharacter();

            Assert.PropertyChanged(_sut, "Health", () => _sut.TakeDamage(10));
        }

        //[Fact]
        //public void TakeZeroDamage()
        //{
        //    _sut.TakeDamage(0);

        //    Assert.Equal(100, _sut.Health);
        //}

        //[Fact]
        //public void TakeSmallDamage()
        //{
        //    _sut.TakeDamage(1);

        //    Assert.Equal(99, _sut.Health);
        //}

        //[Fact]
        //public void TakeMediumDamage()
        //{
        //    _sut.TakeDamage(50);

        //    Assert.Equal(50, _sut.Health);
        //}

        //[Fact]
        //public void TakeMinimum1Health()
        //{
        //    _sut.TakeDamage(101);

        //    Assert.Equal(1, _sut.Health);
        //}

        [Theory]
        //[InlineData(0, 100)]
        //[InlineData(1, 99)]
        //[InlineData(50, 50)]
        //[InlineData(101, 1)]
        
        //[MemberData(nameof(ExternalHealthDamageTestData.TestData), MemberType = typeof(ExternalHealthDamageTestData))]

        [HealthDamageData]
        public void TakeDamage(int damage, int expectedHealth)
        {
            _sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, _sut.Health);
        }
    }
}
