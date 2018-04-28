﻿using System;
using System.IO;

namespace NotificationFramework.Email
{
    public sealed class EmailNotificationAttachment : INotificationAttachment, IDisposable
    {
        private readonly MemoryStream _data;

        private bool _disposedValue = false; // To detect redundant calls

        public string Name { get; }

        public Stream Data
        {
            get
            {
                var data = new MemoryStream();
                this._data.Seek(0, SeekOrigin.Begin);
                this._data.CopyTo(data);
                return data;
            }
        }

        public EmailNotificationAttachment(string name, Stream stream)
        {
            this.Name = name;

            this._data = new MemoryStream();
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(this._data);
        }

        #region IDisposable Support

        void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    this._data.Dispose();
                }

                this._disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}