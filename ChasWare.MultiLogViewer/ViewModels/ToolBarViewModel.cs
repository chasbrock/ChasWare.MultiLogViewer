// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonCommands.cs" company="chas.brock@gmail.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Autofac;
using ChasWare.MultiLogViewer.Common.Helpers;
using ChasWare.MultiLogViewer.Common.ViewModels;
using ChasWare.MultiLogViewer.Interfaces;
using ChasWare.MultiLogViewer.Models;

namespace ChasWare.MultiLogViewer.ViewModels
{
    /// <summary>
    ///     The main menu view model.
    /// </summary>
    public class ToolBarViewModel : BaseDockableViewModel
    {
        #region Constants and fields 

        private readonly ILoggingModel _model;

        #endregion

        #region Constructors

        public ToolBarViewModel() : this(new Log4NetLoggingModel())
        {
        }

        public ToolBarViewModel(IComponentContext context) : this(context.Resolve<ILoggingDetailService>().GetLoggingModel())
        {
        }

        private ToolBarViewModel(ILoggingModel model) : base("Menu")
        {
            _model = model;
            CanClose = false;
            Title = string.Empty;
        }

        #endregion

        #region public properties

        public string Filter { get; set; }
        public DateTime FromDateTime { get; set; }
        public IEnumerable<LoggingLevel> LogLevels => _model.Levels;
        public BasicCommand RefreshFilesCommand { get; set; }
        public DateTime ToDateTime { get; set; }

        #endregion
    }
}