using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BlockTypesTests
{

    [TestClass]
    public class BlocktypesTest
    {

        [TestMethod]
        public void GetTypes()
        {
            BlockType.SummonTypelist();
            BlockType.AssembleTypes();
        }
        [TestMethod]
        public void TestWeaknessesAddedUp()
        {
            List<BlockType> expectedWeakto = new List<BlockType>() { BlockType.Fire, BlockType.Ground };
            List<BlockType> expectedQuadWeak = new List<BlockType>() { BlockType.Fighting };
            List<BlockType> expectedImmune = new List<BlockType>() { BlockType.Ghost, BlockType.Poison };
            List<BlockType> actualWeakto = BlockType.WeaknessAddedUp(BlockType.Normal, BlockType.Steel).WeakTo;
            List<BlockType> actualQuadWeak = BlockType.WeaknessAddedUp(BlockType.Normal, BlockType.Steel).QuadWeak;
            List<BlockType> actualImmune = BlockType.WeaknessAddedUp(BlockType.Normal, BlockType.Steel).Immune;

            CollectionAssert.AreEqual(expectedWeakto, actualWeakto);
            CollectionAssert.AreEqual(expectedQuadWeak, actualQuadWeak);
            CollectionAssert.AreEqual(expectedImmune, actualImmune);
        }
        [TestMethod]
        public void TestSuperEffectiveCoverage()
        {
            List<BlockType> expected = new List<BlockType>() { BlockType.Flying, BlockType.Water };
            List<BlockType> actual = BlockType.SuperEffectiveCoverage(BlockType.Normal, BlockType.Electric).MixedCoverage;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSummonList()
        {
           
            BlockType expect = BlockType.Electric;
            BlockType actual = BlockType.SummonTypelist()[3];
            Assert.AreEqual(expect, actual);
        }
        [TestMethod]
        public void TestName()
        {
            string result = BlockType.ChooseType("water").Name;

            Assert.AreEqual("Water", result);
        }
        [TestMethod]
        public void TestFormDEST()
        {

            List<BlockType> actual = BlockType.FormDSET(BlockType.ChooseType("Electric"));
            List<BlockType> expected = new List<BlockType>() { BlockType.ChooseType("Flying"), BlockType.ChooseType("Water") };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetDEST()
        {
            List<BlockType> expected = new List<BlockType>() { BlockType.ChooseType("Flying"), BlockType.ChooseType("Water") };

            List<BlockType> actual = BlockType.ChooseType("Electric").GetDSET();

            CollectionAssert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(expected, BlockType.ChooseType("Electric").DealsSuperEffectiveTo);
        }
        [TestMethod]
        public void TestGetWt()
        {

            List<BlockType> expected = new List<BlockType> { BlockType.ChooseType("Ground") };

            List<BlockType> actual = BlockType.Electric.WeakTo;

            CollectionAssert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(expected, BlockType.Electric.GetWT());
        }
        [TestMethod]
        public void TestGetRES()
        {
            string expected = BlockType.ElectricGet().Resistances[0].Name;

            string actual = "Flying";
            List<BlockType> collectedExpected = new List<BlockType> { { BlockType.Flying },{ BlockType.Water } };
            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(collectedExpected, BlockType.ElectricGet().GetRES() );
        }
        [TestMethod]
        public void TestChooseType()
        {
            BlockType electric = BlockType.ElectricGet();

            BlockType other = new BlockType();
            BlockType actual = BlockType.ChooseType("electric");

            Assert.AreEqual(electric, actual);
        }
        [TestMethod]
        public void TestChooseType2()
        {
            BlockType electric = BlockType.ChooseType("electric");

            BlockType other = new BlockType();
            BlockType actual = BlockType.ChooseType("electric");

            Assert.AreEqual(electric, actual);
        }
        [TestMethod]
        public void TestChooseType3()
        {
           
           

           

            Assert.AreEqual("Ground", BlockType.Electric.WeakTo[0].Name);

        }
    }
}



