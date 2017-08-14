using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Events.IO.Domain.Core.Commands;
using Events.IO.Domain.Interfaces;
using Events.IO.Infra.Data.Context;

namespace Events.IO.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextEvents _context;

        public UnitOfWork(ContextEvents context)
        {
            _context = context;
        }

        public CommandResponse Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
