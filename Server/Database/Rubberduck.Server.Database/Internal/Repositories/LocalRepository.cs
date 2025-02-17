﻿using System.Data;
using System.Threading.Tasks;
using Dapper;
using Rubberduck.RPC.Platform.Model.Database;
using Rubberduck.Server.Database.Internal.Storage.Abstract;

namespace Rubberduck.Server.Database.Internal.Storage
{
    internal class LocalRepository : Repository<Local>
    {
        public LocalRepository(IDbConnection connection)
            : base(connection) { }

        protected override string Source { get; } = "[Locals]";
        protected override string[] ColumnNames { get; } = new[]
        {
            "DeclarationId",
            "IsAutoAssigned",
            "DeclaredValue",
        };

        public override async Task SaveAsync(Local entity)
        {
            if (entity.Id == default)
            {
                entity.Id = await Database.ExecuteAsync(InsertSql,
                    new
                    {
                        declarationId = entity.DeclarationId,
                        isAutoAssigned = entity.IsAutoAssigned,
                        declaredValue = entity.ValueExpression,
                    });
            }
            else
            {
                await Database.ExecuteAsync(UpdateSql,
                    new 
                    {
                        declarationId = entity.DeclarationId,
                        isAutoAssigned = entity.IsAutoAssigned,
                        declaredValue = entity.ValueExpression,
                        id = entity.Id,
                    });
            }
        }
    }
}
