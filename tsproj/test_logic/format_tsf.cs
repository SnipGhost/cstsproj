namespace tsproj.test_logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class format_tsf
    {
        public Stream baseStream;
        private byte[] bf = new byte[0x10];
        private List<string> precachestrings = new List<string>();

        public format_tsf(Stream file)
        {
            this.baseStream = file;
        }

        public void Close()
        {
            this.precachestrings.Clear();
            this.baseStream.Close();
        }

        public void PrecacheString(string s)
        {
            if (!string.IsNullOrEmpty(s) && !this.precachestrings.Contains(s))
            {
                this.precachestrings.Add(s);
            }
        }

        public bool ReadBool()
        {
            this.baseStream.Read(this.bf, 0, 1);
            return BitConverter.ToBoolean(this.bf, 0);
        }

        public byte ReadByte()
        {
            this.baseStream.Read(this.bf, 0, 1);
            return this.bf[0];
        }

        public double ReadDouble()
        {
            this.baseStream.Read(this.bf, 0, 8);
            return BitConverter.ToDouble(this.bf, 0);
        }

        public float ReadFloat()
        {
            this.baseStream.Read(this.bf, 0, 4);
            return BitConverter.ToSingle(this.bf, 0);
        }

        public void ReadHead()
        {
            if (this.ReadInt() == 0x3dae3)
            {
                int num = this.ReadInt();
                for (int i = 0; i < num; i++)
                {
                    this.precachestrings.Add(this.ReadString_real());
                }
            }
        }

        public int ReadInt()
        {
            this.baseStream.Read(this.bf, 0, 4);
            return BitConverter.ToInt32(this.bf, 0);
        }

        public long ReadLong()
        {
            this.baseStream.Read(this.bf, 0, 8);
            return BitConverter.ToInt64(this.bf, 0);
        }

        public object ReadObj()
        {
            switch (this.ReadByte())
            {
                case 0:
                    return this.ReadBool();

                case 1:
                    return this.ReadByte();

                case 2:
                    return this.ReadShort();

                case 3:
                    return this.ReadInt();

                case 4:
                    return this.ReadLong();

                case 5:
                    return this.ReadFloat();

                case 6:
                    return this.ReadDouble();

                case 7:
                    return this.ReadString();
            }
            return null;
        }

        public short ReadShort()
        {
            this.baseStream.Read(this.bf, 0, 2);
            return BitConverter.ToInt16(this.bf, 0);
        }

        public string ReadString()
        {
            int num = this.ReadInt();
            if ((this.precachestrings.Count > num) && (num > -1))
            {
                return this.precachestrings[num];
            }
            return "";
        }

        public string ReadString_real()
        {
            int count = this.ReadShort() * 2;
            byte[] buffer = new byte[count];
            this.baseStream.Read(buffer, 0, count);
            return Encoding.Unicode.GetString(buffer);
        }

        public void Write(bool b)
        {
            this.baseStream.Write(BitConverter.GetBytes(b), 0, 1);
        }

        public void Write(byte b)
        {
            this.baseStream.WriteByte(b);
        }

        public void Write(double s)
        {
            this.baseStream.Write(BitConverter.GetBytes(s), 0, 8);
        }

        public void Write(short s)
        {
            this.baseStream.Write(BitConverter.GetBytes(s), 0, 2);
        }

        public void Write(int s)
        {
            this.baseStream.Write(BitConverter.GetBytes(s), 0, 4);
        }

        public void Write(long s)
        {
            this.baseStream.Write(BitConverter.GetBytes(s), 0, 8);
        }

        public void Write(object o)
        {
            switch (o.GetType().Name.ToLower())
            {
                case "boolean":
                    this.Write((byte) 0);
                    this.Write((bool) o);
                    break;

                case "byte":
                    this.Write((byte) 1);
                    this.Write((byte) o);
                    break;

                case "int16":
                    this.Write((byte) 2);
                    this.Write((short) o);
                    break;

                case "int32":
                    this.Write((byte) 3);
                    this.Write((int) o);
                    break;

                case "int64":
                    this.Write((byte) 4);
                    this.Write((long) o);
                    break;

                case "single":
                    this.Write((byte) 5);
                    this.Write((float) o);
                    break;

                case "double":
                    this.Write((byte) 6);
                    this.Write((double) o);
                    break;

                case "string":
                    this.Write((byte) 7);
                    this.Write((string) o);
                    break;
            }
        }

        public void Write(float s)
        {
            this.baseStream.Write(BitConverter.GetBytes(s), 0, 4);
        }

        public void Write(string s)
        {
            this.Write(this.precachestrings.IndexOf(s));
        }

        public void Write_real(string s)
        {
            short length = (short) s.Length;
            this.Write(length);
            this.baseStream.Write(Encoding.Unicode.GetBytes(s), 0, length * 2);
        }

        public void WriteHead()
        {
            this.Write(0x3dae3);
            this.Write(this.precachestrings.Count);
            for (int i = 0; i < this.precachestrings.Count; i++)
            {
                this.Write_real(this.precachestrings[i]);
            }
        }
    }
}

