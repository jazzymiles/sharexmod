using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SevenZip.Sdk.Compression.Lzma;

namespace UploadersLibMod
{
    public static class ZipHelper
    {
        public static Stream CompressFileLZMA(Stream inFile)
        {
            Stream outFile = null;
            try
            {
                SevenZip.Sdk.Compression.Lzma.Encoder coder = new SevenZip.Sdk.Compression.Lzma.Encoder();
                outFile = new MemoryStream();
                if (outFile != null && outFile.CanSeek)
                    outFile.Position = 0;

                // Write the encoder properties
                coder.WriteCoderProperties(outFile);

                // Write the decompressed file size.
                outFile.Write(BitConverter.GetBytes(inFile.Length), 0, 8);

                // Encode the file.
                coder.Code(inFile, outFile, inFile.Length, -1, null);

                if (outFile != null && outFile.CanSeek)
                    outFile.Position = 0;
            }
            catch
            {
                return null;
            }
            return outFile;
        }

        public static bool CompressFileLZMA(string inFile, string outFile)
        {
            try
            {
                SevenZip.Sdk.Compression.Lzma.Encoder coder = new SevenZip.Sdk.Compression.Lzma.Encoder();
                FileStream input = new FileStream(inFile, FileMode.Open);
                FileStream output = new FileStream(outFile, FileMode.Create);

                // Write the encoder properties
                coder.WriteCoderProperties(output);

                // Write the decompressed file size.
                output.Write(BitConverter.GetBytes(input.Length), 0, 8);

                // Encode the file.
                coder.Code(input, output, input.Length, -1, null);
                output.Flush();
                output.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool DecompressFileLZMA(string inFile, string outFile)
        {
            try
            {
                SevenZip.Sdk.Compression.Lzma.Decoder coder = new SevenZip.Sdk.Compression.Lzma.Decoder();
                FileStream input = new FileStream(inFile, FileMode.Open);
                FileStream output = new FileStream(outFile, FileMode.Create);

                // Read the decoder properties
                byte[] properties = new byte[5];
                input.Read(properties, 0, 5);

                // Read in the decompress file size.
                byte[] fileLengthBytes = new byte[8];
                input.Read(fileLengthBytes, 0, 8);
                long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

                coder.SetDecoderProperties(properties);
                coder.Code(input, output, input.Length, fileLength, null);
                output.Flush();
                output.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
