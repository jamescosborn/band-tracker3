using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
  [TestClass]
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=3306; database=band_tracker_tests;";
    }
    public void Dispose()
    {
      Band.ClearAll();
    }

    [TestMethod]
    public void GetAll_DatabaseIsEmptyAtFirst_0()
    {
      List<Band> bands = Band.GetAll();
      int result = bands.Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void HasSamePropertiesAs_BothHaveSameProperties_True()
    {
      Band band1 = new Band("Black Sabbath");
      Band band2 = new Band("Black Sabbath");

      bool result = band1.HasSamePropertiesAs(band2);

      Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void HasSamePropertiesAs_BothDontHaveSameProperties_False()
    {
      Band band1 = new Band("Black Sabbath");
      Band band2 = new Band("The Beatles");

      bool result = band1.HasSamePropertiesAs(band2);

      Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void Save_SaveBand_BandSaved()
    {
      Band testBand = new Band("LCD Soundsystem");
      testBand.Save();

      List<Band> bands = Band.GetAll();
      int result = bands.Count;
      Console.WriteLine(result);

      Assert.AreEqual(true,Band.GetAll().Count==1);
    }

    [TestMethod]
    public void Find_GetsSpecificBandFromDatabase_Band()
    {
      Band localBand = new Band("MGMT");
      localBand.Save();
      Band databaseBand = Band.Find(localBand.Id);

      bool result = localBand.HasSamePropertiesAs(databaseBand);

      Assert.AreEqual(true, result);
    }
  }
}
