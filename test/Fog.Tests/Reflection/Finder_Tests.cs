using Fog.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Fog.Tests.Reflection
{
    public class Finder_Tests
    {
        private readonly IFinder _finder;

        public Finder_Tests()
        {
            _finder = new Finder();
        }

        [Fact]
        public void GetAssemblies_ReturnNotEmpty()
        {
            var assemblies = _finder.GetAssemblies();

            Assert.NotNull(assemblies);
            Assert.NotEmpty(assemblies);
        }

        [Fact]
        public void Find_Test_1()
        {
            var types = _finder.Find<IFindA>();
            Assert.Single(types);
            Assert.Contains(typeof(FindA), types);
        }

        [Fact]
        public void Find_Test_2()
        {
            var types = _finder.Find<IFindB>();

            Assert.Equal(2, types.Count);
            Assert.Contains(typeof(FindA), types);
            Assert.Contains(typeof(FindB), types);
        }

        [Fact]
        public void Find_Test_3()
        {
            var types = _finder.Find<IFindC>();

            Assert.Equal(2, types.Count);
            Assert.Contains(typeof(FindB), types);
            Assert.Contains(typeof(FindD<>), types);
        }

        [Fact]
        public void Find_Test_4()
        {
            var types = _finder.Find<IFindE<FindE>>();

            Assert.Single(types);
            Assert.Contains(typeof(FindE), types);
        }

        [Fact]
        public void Find_Test_5()
        {
            var types = _finder.Find(typeof(IFindE<>));

            Assert.Equal(2, types.Count);
            Assert.Contains(typeof(FindE), types);
            Assert.Contains(typeof(FindF<>), types);
        }

        [Fact]
        public void Find_Test_Concurrency()
        {
            Fog.Helpers.Thread.WaitAll(() =>
                {
                    var types = _finder.Find<IFindA>();
                    Assert.Single(types);
                    Assert.Contains(typeof(FindA), types);
                }, () => {
                    var types = _finder.Find<IFindB>();
                    Assert.Equal(2, types.Count);
                    Assert.Contains(typeof(FindA), types);
                    Assert.Contains(typeof(FindB), types);
                }, () =>
                {
                    var types = _finder.Find<IFindC>();
                    Assert.Equal(2, types.Count);
                    Assert.Contains(typeof(FindB), types);
                    Assert.Contains(typeof(FindD<>), types);
                });
        }
    }

    public interface IFindA { }

    public interface IFindB { }

    public interface IFindC { }

    public interface IFindE<T> { }

    public class FindA : IFindA, IFindB { }

    public class FindB : IFindB, IFindC { }

    public abstract class FindC : IFindB, IFindC { }

    public class FindD<T> : IFindC { }

    public class FindE: IFindE<FindE> { }

    public class FindF<T> : IFindE<T> { }
}