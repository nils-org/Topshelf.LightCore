using FluentAssertions;
using LightCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Topshelf.LightCore;

namespace Topshelf.LightCore.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var startCalled = false;
            var stopCalled = false;
            var diWasCalled = false;

            var builder = new ContainerBuilder();
            builder.Register(x =>
            {
                diWasCalled = true;
                return new FakeSevice(
                    () => startCalled = true,
                    () => stopCalled = true);
            });
            var container = builder.Build();

            var host = HostFactory.New(x =>
            {
                x.ApplyCommandLine(string.Empty); // fake "normal" start
                x.UseLightCore(container);
                x.Service<FakeSevice>(s =>
                {
                    s.ConstructUsingLightCore<FakeSevice>();
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                    s.AfterStartingService(tc =>
                    {
                        // ensure we're stopping again...
                        tc.Stop();
                    });
                });
            });

            // act
            var actual = host.Run();

            // assert
            actual.Should().Be(TopshelfExitCode.Ok);
            diWasCalled.Should().BeTrue();
            startCalled.Should().BeTrue();
            stopCalled.Should().BeTrue();
        }
    }

    internal class FakeSevice
    {
        private readonly Action start;
        private readonly Action stop;

        internal FakeSevice(Action start, Action stop)
        {
            this.start = start;
            this.stop = stop;
        }

        public void Start()
        {
            start();
        }

        public void Stop()
        {
            stop();
        }
    }
}
