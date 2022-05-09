using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ExceptionVsMediatrBenchmarks

{
    internal static class Program
    {
        public static void Main(string[] args)
        {

            var summary = BenchmarkRunner.Run(typeof(Benchmarks).Assembly);
        }
    }

    [MemoryDiagnoser]
    public class Benchmarks
    {
        private IMediator _mediatr;

        [GlobalSetup]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(Benchmarks));

            var provider = services.BuildServiceProvider();
            _mediatr = provider.GetRequiredService<IMediator>();
        }

        [Benchmark]
        public string WithExceptionHandler()
        {
            try
            {
                var id = 1;
                if (id == 1)
                    throw new ArgumentNullException();
            }
            catch
            {

                return "1";
            }
            return "2";
        }

        [Benchmark]
        public string WithMediatr()
        {
            _mediatr.Send(new MediatrRequest(1));
            return "2";
        }
    }

    public class MediatrRequest : IRequest<string>
    {
        public MediatrRequest(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class MediatrRequestHandler : IRequestHandler<MediatrRequest, string>
    {
        public async Task<string> Handle(MediatrRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == 1)
                return await Task.FromResult("1");
            else
                return await Task.FromResult("2");
        }
    }
}
