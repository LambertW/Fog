using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fog.Data.UnitOfWork
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly List<IUnitOfWork> _unitOfWorks;

        public UnitOfWorkManager()
        {
            _unitOfWorks = new List<IUnitOfWork>();
        }

        public void Commit()
        {
            foreach (var unitOfWork in _unitOfWorks)
            {
                unitOfWork.Commit();
            }
        }

        public async Task CommitAsync()
        {
            foreach (var unitOfWork in _unitOfWorks)
            {
                await unitOfWork.CommitAsync();
            }
        }

        public void Register(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            if (!_unitOfWorks.Contains(unitOfWork))
                _unitOfWorks.Add(unitOfWork);
        }
    }
}
