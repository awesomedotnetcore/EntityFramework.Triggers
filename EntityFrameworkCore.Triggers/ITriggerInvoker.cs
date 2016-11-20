using System;
using System.Collections.Generic;

#if EF_CORE
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace EntityFrameworkCore.Triggers {
#else
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using EntityEntry = System.Data.Entity.Infrastructure.DbEntityEntry;
namespace EntityFramework.Triggers {
#endif
	internal interface ITriggerInvoker {
		List<Action<DbContext>> RaiseTheBeforeEvents(DbContext dbContext);
		void RaiseTheBeforeEventInner(DbContext dbContext, EntityEntry entry, List<EntityEntry> triggeredEntries, List<Action<DbContext>> afterEvents, ref Boolean cancel);

		void RaiseTheAfterEvents(DbContext dbContext, IEnumerable<Action<DbContext>> afterEvents);

		void RaiseTheFailedEvents(DbContext dbContext, DbUpdateException dbUpdateException, ref Boolean swallow);
#if !EF_CORE
		void RaiseTheFailedEvents(DbContext dbContext, DbEntityValidationException dbEntityValidationException, ref Boolean swallow);
#endif
		void RaiseTheFailedEvents(DbContext dbContext, Exception exception, ref Boolean swallow);
	}
}