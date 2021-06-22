using LightCore;
using System;

using Shouldly;

using Xunit;

namespace Topshelf.LightCore.Tests
{
    public class UnitTest1
    {
        [Fact]
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
                return new FakeService(
                    () => startCalled = true,
                    () => stopCalled = true);
            });
            var container = builder.Build();

            var host = HostFactory.New(x =>
            {
                x.ApplyCommandLine(string.Empty); // fake "normal" start
                x.UseLightCore(container);
                x.Service<FakeService>(s =>
                {
                    s.ConstructUsingLightCore();
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
            actual.ShouldBe(TopshelfExitCode.Ok);
            diWasCalled.ShouldBeTrue();
            startCalled.ShouldBeTrue();
            stopCalled.ShouldBeTrue();
        }
    }

    internal class FakeService
    {
        private readonly Action start;
        private readonly Action stop;

        internal FakeService(Action start, Action stop)
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
