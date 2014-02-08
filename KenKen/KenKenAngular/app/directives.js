'use strict';
var kenkenDirectives = angular.module('kenkenDirectives', []);

kenkenDirectives.directive('ngFocusWhenSelected', function ($timeout) {
    return function (scope, element) {
        // 1 ms timeout required or the text within is not selected
        $timeout(function () {
            element[0].select();
        }, 1);
    };
});
kenkenDirectives.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        var enterKeyCode = 13;

        element.bind("keydown keypress", function (event) {
            if (event.which === enterKeyCode) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});
kenkenDirectives.directive('ngDownArrow', function () {
    return function (scope, element, attrs) {
        var downArrowKeyCode = 40;

        element.bind("keydown keypress", function (event) {
            if (event.which === downArrowKeyCode) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngDownArrow);
                });

                event.preventDefault();
            }
        });
    };
});
kenkenDirectives.directive('ngUpArrow', function () {
    return function (scope, element, attrs) {
        var upArrowKeyCode = 38;

        element.bind("keydown keypress", function (event) {
            if (event.which === upArrowKeyCode) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngUpArrow);
                });

                event.preventDefault();
            }
        });
    };
});
kenkenDirectives.directive('ngLeftArrow', function () {
    return function (scope, element, attrs) {
        var leftArrowKeyCode = 37;

        element.bind("keydown keypress", function (event) {
            if (event.which === leftArrowKeyCode) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngLeftArrow);
                });

                event.preventDefault();
            }
        });
    };
});
kenkenDirectives.directive('ngRightArrow', function () {
    return function (scope, element, attrs) {
        var rightArrowKeyCode = 39;

        element.bind("keydown keypress", function (event) {
            if (event.which === rightArrowKeyCode) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngRightArrow);
                });

                event.preventDefault();
            }
        });
    };
});
//# sourceMappingURL=directives.js.map
