using System.Text;
using System.Threading.Tasks;
using Qrist.Infrastructure.Gzip;

namespace Qrist.Tests.Unit
{
    public class GzipCompressorTests
    {
        private readonly TestContext _context = new();

        [Test]
        public async Task Test_Basic_String_Compression()
        {
            _context
                .ArrangeTestData();

            await
                _context
                    .ActCompressData();

            await
                _context
                    .ActDecompressData();

            _context
                .AssertDataIsTheSame();
        }

        private class TestContext
        {
            private readonly GzipCompressor _sut = new();
            private byte[] _rawData;
            private byte[] _compressedData;
            private byte[] _result;

            public void ArrangeTestData()
            {
                _rawData =
                    Encoding
                        .Default
                        .GetBytes("Hello World");
            }

            public async Task ActCompressData() =>
                _compressedData =
                    await
                        _sut
                            .CompressAsync(_rawData);

            public async Task ActDecompressData() =>
                _result =
                    await
                        _sut
                            .DecompressAsync(_compressedData);

            public void AssertDataIsTheSame() =>
                Assert.That(_result, Is.EqualTo(_rawData));
        }
    }
}