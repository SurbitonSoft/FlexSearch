/// <reference path="../../references/references.d.ts" />

module flexportal {
  'use strict';

  export class IndexDetailedResult extends IndexResult {
    DocCount: number
    DiskSize: number
  }

  export interface IClusterScope extends ng.IScope, IMainScope {
    ChartsData: { Data: number[]; Labels: string[] }[]
    ChartsDataStore: { Data: number[]; Labels: string[] }[]
    Charts: any[]
    prettysize(s,n,o): string
    rerender(chart: any, show: boolean): void
    Indices: IndexDetailedResult[]
    MemoryDetails: FlexSearch.Core.MemoryDetailsResponse
    RadarChart: LinearInstance
    BarChart: LinearInstance
    IndicesPromise: ng.IPromise<void>
    FlexSearchUrl : string
    
    // Shows which small chart is being displayed on the right column
    Rendering: string
    
    // Goes to the details page of the given index
    showDetails(indexName): void
  }

  var colors = ["125, 188, 219", "125, 219, 144", "167, 125, 219",
    "219, 125, 175", "232, 112, 84", "182, 169, 31", "48, 71, 229"]

  export class ClusterController {
    private static unusedColors = colors.slice();

    private static getNextColor() {
      if(this.unusedColors.length > 0)
        return this.unusedColors.pop();
        
      return "0,0,0";
    }

    private static populateDocCount($scope: IClusterScope, dsIdx, dataIdx, value) {
      //$scope.RadarData.datasets[dsIdx].data[dataIdx] = value;
      (<any>$scope.RadarChart).datasets[dsIdx].points[dataIdx].value = 100;
      
      $scope.RadarChart.update();
    }
    
    private static toPercentage(array : number[]) {
      var max = Math.max.apply(null, array);
      if(max == 0) return array;
      
      return array.map(x => Math.floor(x/max * 100));
    }
    
    private static createChart(type, canvas, chartVar, data, options?) {
      // Create the chart
      switch (type.toLowerCase()) {
        case "radar": 
          chartVar = new Chart((<any>canvas.get(0)).getContext("2d")).Radar(data, options);
          break; 
        case "bar":
          chartVar = new Chart((<any>canvas.get(0)).getContext("2d")).Bar(data, options);
          break;
      }
      
      // Create the legend
      canvas.parent().append(
        '<chart-legend>' + chartVar.generateLegend() + '</chart-legend>');
    }
    
    private static getPrettySizeFunc() {
       var sizes = [
         'B', 'kB', 'MB', 'GB', 'TB', 'PB', 'EB'
       ];
      
       /**
         Pretty print a size from bytes
       @method pretty
       @param {Number} size The number to pretty print
       @param {Boolean} [nospace=false] Don't print a space
       @param {Boolean} [one=false] Only print one character
       */
      
       var prettysize = function(size, nospace, one) {
         var mysize, f;
      
         sizes.forEach(function(f, id) {
           if (one) {
             f = f.slice(0, 1);
           }
           var s = Math.pow(1024, id),
           fixed;
           if (size >= s) {
             fixed = String((size / s).toFixed(1));
             if (fixed.indexOf('.0') === fixed.length - 2) {
               fixed = fixed.slice(0, -2);
             }
             mysize = fixed + (nospace ? '' : ' ') + f;
           }
         });
      
         // zero handling
         // always prints in Bytes
         if (!mysize) {
           f = (one ? sizes[0].slice(0, 1) : sizes[0]);
           mysize = '0' + (nospace ? '' : ' ') + f;
         }
      
         return mysize;
       };
       
       return prettysize;
    }

    private static GetIndicesData(flexClient: FlexClient, $scope: IClusterScope) {
      return flexClient.getIndices()
        .then(response => $scope.Indices = <IndexDetailedResult[]>response)
        
        // Get the number of documents in each index
        .then(() => flexClient.resolveAllPromises(
            $scope.Indices.map(i => flexClient.getDocsCount(i.IndexName))))
        // Store the number of documents on the main Index Store
        .then(docCounts => $scope.Indices.forEach((idx, i) => idx.DocCount = docCounts[i]))
        
        // Get the indices disk size
        .then(() => flexClient.resolveAllPromises(
          $scope.Indices.map(i => flexClient.getIndexSize(i.IndexName))))
        // Store the disk size of the indices
        .then(sizes => {
          $scope.Indices.forEach((idx, i) => idx.DiskSize = sizes[i]);
          $scope.ChartsDataStore['disk'] = {
            Data: [
              $scope.Indices
                .map(i => i.DiskSize)
                .reduce((acc, val) => acc + val, 0),
              1000], // 1000 MB in total TODO
            Labels: ["Used", "Free"]
          };
        })
        
        // Get the memory details
        .then(() => flexClient.getMemoryDetails())
        // Store the memory details
        .then(mem => {
          $scope.MemoryDetails = mem;
          $scope.ChartsDataStore['memory'] = {
            Data: [mem.UsedMemory, mem.TotalMemory - mem.UsedMemory],
            Labels: ["Used", "Free"] };
        })
        
        // Create the Radar Chart
        .then(() => {
          // Compute everything to percentage
          var sizes = ClusterController.toPercentage(
            $scope.Indices.map(i => i.DiskSize));
          var docs = ClusterController.toPercentage(
            $scope.Indices.map(i => i.DocCount));
          var shards = ClusterController.toPercentage(
            $scope.Indices.map(i => parseInt(i.ShardConfiguration.ShardCount)));
          var profiles = ClusterController.toPercentage(
            $scope.Indices.map(i => i.SearchProfiles.length));
          var fields = ClusterController.toPercentage(
            $scope.Indices.map(i => i.Fields.length));
          
          var radarData : LinearChartData = {
            labels: ["Size", "Shards", "Profiles", "Fields", "Docs"],
            datasets: [] }; 
            
          $scope.Indices.forEach((index, i, idxs) => {
            var nextColor = ClusterController.getNextColor();
            
            var ds = {
              label: index.IndexName,
              fillColor: "rgba(" + nextColor + ",0.2)",
              strokeColor: "rgba(" + nextColor + ",1)",
              data: [
                sizes[i],
                shards[i],
                profiles[i],
                fields[i],
                docs[i],
              ]
            };
            
            radarData.datasets.push(ds);
          })
          
          ClusterController.createChart("radar", $('#overall'), $scope.RadarChart, 
            radarData, { responsive: false });
        })
        // Create the Bar Chart
        .then(() => {
          var barData : LinearChartData = {
            labels: $scope.Indices.map(i => i.IndexName),
            datasets: [{
                label: "Number of documents",
                fillColor: "rgba(151,187,205,0.5)",
                strokeColor: "rgba(151,187,205,0.8)",
                highlightFill: "rgba(151,187,205,0.75)",
                highlightStroke: "rgba(151,187,205,1)",
                data: $scope.Indices.map(i => i.DocCount)
              }]
          };
          
          ClusterController.createChart("bar", $('#docs'), $scope.BarChart, 
            barData, { responsive: false });
        });
    }
    
    /* @ngInject */
    constructor($scope: IClusterScope, $state: any, $timeout: ng.ITimeoutService, flexClient: FlexClient) {
      $scope.Rendering = null;
      $scope.FlexSearchUrl = FlexSearchUrl;
      $scope.prettysize = ClusterController.getPrettySizeFunc();
      
      // Clear the chart binding data if window is resized
      $(window).resize(function() {
        $scope.Charts.forEach((c, i) => c.destroy());
        $scope.Charts = [];
        $scope.ChartsData = [];
      });

      var getChartById = function(chartId) {
        var filtered = $scope.Charts
          .filter(c => c.chart.canvas.id == chartId);

        if (filtered.length == 0) {
          $scope.showError("Couldn't find chart with ID " + chartId);
          return null;
        }

        return filtered[0];
      }

      $scope.ChartsData = [];
      $scope.Charts = [];
      $scope.$on('create', function(event, data) {
        $scope.Charts.push(data);
      });

      // Get the data for the charts
      $scope.IndicesPromise = ClusterController.GetIndicesData(flexClient, $scope);

      $scope.ChartsDataStore = [];
      $scope.ChartsDataStore['indices'] = {
        Data: [4, 1, 1],
        Labels: ["Online", "Recovering", "Offline"]
      };
      
      $scope.rerender = function(chartName, show) {
        if (show) {
          // Destroy the charts if they exist
          if ($scope.ChartsData[chartName] != undefined) {
            $scope.Charts.forEach((c, i) => c.destroy());
            $scope.Charts = [];
            $scope.ChartsData = [];
          }    
          // Rebuild the chart
          $timeout(function() {
            $scope.ChartsData[chartName] = $scope.ChartsDataStore[chartName];
            $scope.Rendering = chartName;
          });
        }
        else {
          $scope.Rendering = null;
        }
      }
      
      $scope.showDetails = function (indexName) {
        $state.go("indexDetails", {indexName: indexName});
      }
    }
  }
}