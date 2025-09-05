using System.Text;
using System.Threading.Tasks;
using Qrist.Infrastructure.Compression.Brotli;

namespace Qrist.Tests.Unit
{
    public class BrotliCompressorTests
    {
        private readonly TestContext _context = new();

        [Test]
        public async Task Test_Basic_String_Compression()
        {
            _context
                .ArrangeBasicTestData();

            await
                _context
                    .ActCompressData();

            await
                _context
                    .ActDecompressData();

            _context
                .AssertDataIsTheSame();
        }

        [Test]
        public async Task Test_Json_String_Compression()
        {
            _context
                .ArrangeJsonTestData();

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
            private readonly BrotliCompressor _sut = new();
            private byte[] _rawData;
            private byte[] _compressedData;
            private byte[] _result;

            public void ArrangeBasicTestData()
            {
                _rawData =
                    Encoding
                        .Default
                        .GetBytes("Hello World");
            }

            public void ArrangeJsonTestData()
            {
                _rawData =
                    Encoding
                        .Default
                        .GetBytes("{\"provider\":\"TODOIST\",\"data\":{\"tasks\":[{\"description\":\"chives\",\"labels\":[\"shopping\"]},{\"description\":\"1/2 cup olives\",\"labels\":[\"shopping\"]},{\"description\":\"maryland chicken thighs, deboned\",\"labels\":[\"shopping\"]}]}}");
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