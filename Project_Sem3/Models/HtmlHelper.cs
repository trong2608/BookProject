﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project_Sem3.Models
{
        public static class HtmlHelperFactoryExtensions
        {
            public static HtmlHelper<TModel> For<TModel>(this HtmlHelper helper) where TModel : class, new()
            {
                return For<TModel>(helper.ViewContext, helper.ViewDataContainer.ViewData, helper.RouteCollection);
            }

            public static HtmlHelper<TModel> For<TModel>(this HtmlHelper helper, TModel model)
            {
                return For<TModel>(helper.ViewContext, helper.ViewDataContainer.ViewData, helper.RouteCollection, model);
            }

            public static HtmlHelper<TModel> For<TModel>(ViewContext viewContext, ViewDataDictionary viewData, RouteCollection routeCollection) where TModel : class, new()
            {
                TModel model = new TModel();
                return For<TModel>(viewContext, viewData, routeCollection, model);
            }

            public static HtmlHelper<TModel> For<TModel>(ViewContext viewContext, ViewDataDictionary viewData, RouteCollection routeCollection, TModel model)
            {
                var newViewData = new ViewDataDictionary(viewData) { Model = model };
                ViewContext newViewContext = new ViewContext(
                    viewContext.Controller.ControllerContext,
                    viewContext.View,
                    newViewData,
                    viewContext.TempData,
                    viewContext.Writer);
                var viewDataContainer = new ViewDataContainer(newViewContext.ViewData);
                return new HtmlHelper<TModel>(newViewContext, viewDataContainer, routeCollection);
            }

            private class ViewDataContainer : System.Web.Mvc.IViewDataContainer
            {
                public System.Web.Mvc.ViewDataDictionary ViewData { get; set; }

                public ViewDataContainer(System.Web.Mvc.ViewDataDictionary viewData)
                {
                    ViewData = viewData;
                }
            }



            // PAGINATIONS
        }
    }
