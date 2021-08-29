using Blog.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Email
{
    public interface IEmailSender
    {
        void Send(SendEmailDto dto);
    }
}
