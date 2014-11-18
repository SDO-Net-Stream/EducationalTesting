using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.DataProvider
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public void Begin()
        {
            // TODO: start transaction
        }

        public void Cancel()
        {
            // TODO: rollback transaction
        }

        public void End()
        {
            // TODO: commit transaction, invoke onSuccess handlers
        }

        private bool _isTransactional;
        public void Initialize(bool isTransactional)
        {
            _isTransactional = isTransactional;
        }

        public bool IsTransactional
        {
            get
            {
                return _isTransactional; 
            }
        }

        public void OnSuccess(Action action)
        {
            // TODO: register handler
        }

        public void SaveChanges()
        {
            //
        }

        public void Dispose()
        {
            
        }
    }
}
