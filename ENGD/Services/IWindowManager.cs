// SPDX-License-Identifier: GPL-3.0-or-later

using System.Threading;
using System.Threading.Tasks;

namespace ENGD.Services
{
    public interface IWindowManager
    {
        Task<TResult> ShowDialogAsync<TResult>(object viewModel, CancellationToken cancellationToken = default);
    }
}
