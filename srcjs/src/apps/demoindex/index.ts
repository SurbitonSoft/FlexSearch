/// <reference path="../../../typings/tsd.d.ts" />

/// <reference path="../../common/partials/main.controller.ts" />
/// <reference path="../../common/client/api.d.ts" />
/// <reference path="../../common/references/references.d.ts" />


/// <reference path="demoindex.ts" />

module flexportal {
  'use strict';

  angular.module('flexportal', ['ngAnimate', 'ngTouch', 'ngSanitize', 'restangular', 'ngMaterial',
    'ui.router', 'chart.js', 'jsonFormatter', 'swaggerUi', 'ui.grid', 'ui.grid.selection', 
    'ui.grid.pagination', 'ui.grid.exporter', 'ui.ace', 'ui.grid.resizeColumns', 
    'ngMdIcons'])
  // Controllers
    .controller('MainCtrl', ["$scope", "$mdUtil", "$mdSidenav", "$mdBottomSheet", MainCtrl])
    .controller('ErrorController', ErrorController)
    .controller('DemoIndexController', ["$scope", "$state", "flexClient", DemoIndexController])
  // Services
    .service('flexClient', ["$http", "$mdBottomSheet", "$q", "$location", function($http, $mdBottomSheet, $q, $location) { return new FlexClient($http, $mdBottomSheet, $q, $location); }])
    
  // Theming
    .config(function($mdThemingProvider: ng.material.IThemingProvider) {
      $mdThemingProvider.theme('default')
        .primaryPalette('blue')
        .accentPalette('grey');
    })
    
  // Route configuration
    .config(function($stateProvider: angular.ui.IStateProvider, $urlRouterProvider: angular.ui.IUrlRouterProvider) {
      $urlRouterProvider.otherwise("/demoindex");

      $stateProvider
          
      // Session URLs
        .state('main', {
          url: "/main",
          templateUrl: "partials/main.html",
          controller: 'MainCtrl'
        })
        
      .state('demoindex', {
          url: "^/demoindex",
          parent: 'main',
          controller: 'DemoIndexController',
          templateUrl: "demoindex.html"
        })
    });
}