using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Fog.Tests.Domain.Entities
{
    public class Tree_Tests
    {
        [Fact]
        public void InitPath_1Level_Tests()
        {
            var id = Guid.NewGuid();
            var organ = new Organ(id);
            organ.InitPath();

            Assert.Equal(1, organ.Level);
            Assert.Equal($"{id},", organ.Path);
        }

        [Fact]
        public void InitPath_2Level_Tests()
        {
            var parentId = Guid.NewGuid();
            var parent = new Organ(parentId);
            parent.InitPath();

            var childId = Guid.NewGuid();
            var child = new Organ(childId);
            child.InitPath(parent);

            Assert.Equal(2, child.Level);
            Assert.Equal($"{parentId},{childId},", child.Path);
        }

        [Fact]
        public void InitPath_3Level_Tests()
        {
            var oneId = Guid.NewGuid();
            var one = new Organ(oneId);
            one.InitPath();

            var twoId = Guid.NewGuid();
            var two = new Organ(twoId);
            two.InitPath(one);

            var threeId = Guid.NewGuid();
            var three = new Organ(threeId);
            three.InitPath(two);

            Assert.Equal(3, three.Level);
            Assert.Equal($"{oneId},{twoId},{threeId},", three.Path);
        }
    }
}
