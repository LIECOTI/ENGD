// SPDX-License-Identifier: GPL-3.0-or-later

using System.Threading;
using System.Threading.Tasks;

namespace ENGD.Services
{
    public interface IFileService
    {
        Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default);
        Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken = default);
    }
}
