﻿namespace HaloDevelopmentExtender
{
    using System;
    using System.IO;

    public class EndianIO
    {
        private EndianReader _in;
        private EndianWriter _out;
        private readonly EndianType endiantype;
        private readonly string filepath;
        private readonly bool isfile;
        private bool isOpen;
        private System.IO.Stream stream;

        public EndianIO(byte[] Buffer, EndianType EndianStyle)
        {
            this.filepath = "";
            this.endiantype = EndianType.LittleEndian;
            this.endiantype = EndianStyle;
            this.stream = new MemoryStream(Buffer);
            this.isfile = false;
        }

        public EndianIO(System.IO.Stream Stream, EndianType EndianStyle)
        {
            this.filepath = "";
            this.endiantype = EndianType.LittleEndian;
            this.endiantype = EndianStyle;
            this.stream = Stream;
            this.isfile = false;
        }

        public EndianIO(string FilePath, EndianType EndianStyle)
        {
            this.filepath = "";
            this.endiantype = EndianType.LittleEndian;
            this.endiantype = EndianStyle;
            this.filepath = FilePath;
            this.isfile = true;
        }

        public void Close()
        {
            if (this.isOpen)
            {
                this.stream.Close();
                this._in.Close();
                this._out.Close();
                this.isOpen = false;
            }
        }

        public void Open()
        {
            if (!this.isOpen)
            {
                if (this.isfile)
                {
                    this.stream = new FileStream(this.filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                }
                this._in = new EndianReader(this.stream, this.endiantype);
                this._out = new EndianWriter(this.stream, this.endiantype);
                this.isOpen = true;
            }
        }

        public void SeekTo(int offset)
        {
            this.SeekTo(offset, SeekOrigin.Begin);
        }

        public void SeekTo(uint offset)
        {
            this.SeekTo((int) offset, SeekOrigin.Begin);
        }

        public void SeekTo(int offset, SeekOrigin SeekOrigin)
        {
            this.stream.Seek((long) offset, SeekOrigin);
        }

        public bool Closed
        {
            get
            {
                return !this.isOpen;
            }
        }

        public EndianReader In
        {
            get
            {
                return this._in;
            }
        }

        public bool Opened
        {
            get
            {
                return this.isOpen;
            }
        }

        public EndianWriter Out
        {
            get
            {
                return this._out;
            }
        }

        public System.IO.Stream Stream
        {
            get
            {
                return this.stream;
            }
        }
    }
}

