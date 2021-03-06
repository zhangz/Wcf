﻿using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Seterlund.Wcf.Core;
using NUnit.Framework;
using Seterlund.Wcf.WIF;

namespace Seterlund.Wcf.UnitTests
{
    [TestFixture]
    public class ChannelFactoryExtensionTests
    {
        [Test]
        public void IsFederated_BindingIsWS2007FederationHttpBinding_ReturnsTrue()
        {
            // Arrange
            var channelFactory = new ChannelFactory<IService>(new WS2007FederationHttpBinding(), new EndpointAddress("http://localhost"));

            // Act
            var actual = channelFactory.IsFederated();

            // Assert
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void IsFederated_BindingIsBasicHttpBinding_ReturnsFalse()
        {
            // Arrange
            var channelFactory = new ChannelFactory<IService>(new BasicHttpBinding(), new EndpointAddress("http://localhost"));

            // Act
            var actual = channelFactory.IsFederated();

            // Assert
            Assert.AreEqual(false, actual);
        }

        [Test]
        public void IsConfiguredAsFederated_ConfiguredIsCalled_ReturnsTrue()
        {
            // Arrange
            var channelFactory = new ChannelFactory<IService>(new BasicHttpBinding(), new EndpointAddress("http://localhost"));
            channelFactory.ConfigureChannelFactory();

            // Act
            var actual = channelFactory.IsConfiguredAsFederated();

            // Assert
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void IsConfiguredAsFederated_ConfiguredIsNotCalled_ReturnsFalse()
        {
            // Arrange
            var channelFactory = new ChannelFactory<IService>(new BasicHttpBinding(), new EndpointAddress("http://localhost"));

            // Act
            var actual = channelFactory.IsConfiguredAsFederated();

            // Assert
            Assert.AreEqual(false, actual);
        }

        [Test]
        public void SetClientCredentials_WhenCalled_SetsNewClientCredentials()
        {
            // Arrange
            var channelFactory = new ChannelFactory<IService>(new BasicHttpBinding(), new EndpointAddress("http://localhost"));
            var expectedCredentials = new ClientCredentials();
            expectedCredentials.UserName.UserName = "SomeUser";

            // Act
            channelFactory.SetClientCredentials(expectedCredentials);

            // Assert
            Assert.AreEqual(expectedCredentials, channelFactory.Credentials);
            Assert.AreEqual(expectedCredentials.UserName.UserName, channelFactory.Credentials.UserName.UserName);
        }

    }
}
