using System;
using System.Linq;
using System.Threading;
using Kiwi.Api;
using ChirpNestCommunication.Mapping;
using ChirpNestCommunication.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Shouldly.Configuration;

namespace ChirpNestCommunication.Test.Mapping
{
    [TestClass]
    public class ListDeviceResponseMappingTest
    {
        private ListDeviceResponseMapping _testee;


        [TestInitialize]
        public void Initialize()
        {
            _testee = new ListDeviceResponseMapping();
        }

        [TestMethod]
        public void Map_WhenTwoDevicesAreRegistered_ThenTheDevicesAreReturnedAsAList()
        {
            var testObject = new ListDeviceResponse
            {
                NumberOfDevices = 2,
                Devices =
                {
                    new DeviceListItem
                    {
                        Description = "test1",
                        DevEui = "123",
                        DeviceInfoAvailable = true,
                        DeviceType = "19.00", //ADT1
                        FirstMeasurementTime = new Timestamp
                        {
                            Seconds = 60
                        },
                        LastMeasurementTime = new Timestamp
                        {
                            Seconds = 120
                        },
                        Name = "Test device 1",
                        NumberOfMeasurements = 5,
                        SerialNumber = 234
                    },
                    new DeviceListItem
                    {
                        Description = "test2",
                        DevEui = "321",
                        DeviceInfoAvailable = true,
                        DeviceType = "19.00", //ADT1
                        FirstMeasurementTime = new Timestamp
                        {
                            Seconds = 3600
                        },
                        LastMeasurementTime = new Timestamp
                        {
                            Seconds = 7200
                        },
                        Name = "Test device 2",
                        NumberOfMeasurements = 10,
                        SerialNumber = 432
                    }
                }
            };
            var result = _testee.Map(testObject);

            result.Count.ShouldBe(2);
            var resultFirstDevice = result.FirstOrDefault(x => x.Name == "Test device 1");
            resultFirstDevice.ShouldNotBeNull();
            resultFirstDevice.Description.ShouldBe("test1");
            resultFirstDevice.DeviceType.ShouldBe("19.00");
            resultFirstDevice.FirstMeasurement.ShouldBe(new DateTime(1970, 1, 1, 0, 1, 0));
            resultFirstDevice.LastMeasurement.ShouldBe(new DateTime(1970, 1, 1, 0, 2, 0));
            resultFirstDevice.EUI.ShouldBe("123");
            resultFirstDevice.NumberOfMeasurements.ShouldBe(5);
            resultFirstDevice.SerialNumber.ShouldBe("234");
            resultFirstDevice.UniqueSerialNumber.ShouldBe("EUI-123"); // Generated by KELLER helper class based on device type
            var resultSecondDevice = result.FirstOrDefault(x => x.Name == "Test device 2");
            resultSecondDevice.ShouldNotBeNull();
            resultSecondDevice.Description.ShouldBe("test2");
            resultSecondDevice.DeviceType.ShouldBe("19.00");
            resultSecondDevice.FirstMeasurement.ShouldBe(new DateTime(1970, 1, 1, 1, 0, 0));
            resultSecondDevice.LastMeasurement.ShouldBe(new DateTime(1970, 1, 1, 2, 0, 0));
            resultSecondDevice.EUI.ShouldBe("321");
            resultSecondDevice.NumberOfMeasurements.ShouldBe(10);
            resultSecondDevice.SerialNumber.ShouldBe("432");
            resultSecondDevice.UniqueSerialNumber.ShouldBe("EUI-321"); // Generated by KELLER helper class based on device type
        }
    }
}
