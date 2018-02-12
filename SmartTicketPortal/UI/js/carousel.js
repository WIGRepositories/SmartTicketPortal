(function () {
    function folderCtrl($scope) {
        $scope.w = window.innerWidth;
        $scope.h = window.innerHeight;
        $scope.uri = "http://lorempixel.com";
        $scope.folders = ['abstract', 'animals', 'business', 'cats', 'city', 'food', 'night', 'life', 'fashion', 'people', 'nature', 'sports', 'technics', 'transport'];
        $scope.images = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
        $scope.currentFolder = $scope.folders[0];
        $scope.selectFolder = function (folder) {
            $scope.currentFolder = folder;
        };
        $scope.activeFolder = function (folder) {
            return (folder === $scope.currentFolder);
        };
    }
    angular
    .module('carousel', [])
    .controller('folderCtrl', folderCtrl);
}());