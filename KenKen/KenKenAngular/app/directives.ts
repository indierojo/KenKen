'use strict';

var kenkenDirectives = angular.module('kenkenDirectives', []);

kenkenDirectives.directive('ngFocusWhenSelected', ($timeout:ng.ITimeoutService) => (scope, element)=> {
    // 1 ms timeout required or the text within is not selected
    $timeout(()=> {
        element[0].select();
    }, 1);
});

kenkenDirectives.directive('ngEnter', () => (scope, element, attrs) => {
    var enterKeyCode: number = 13;

    element.bind("keydown keypress", event=> {
        if (event.which === enterKeyCode) {
            scope.$apply(() => {
                scope.$eval(attrs.ngEnter);
            });

            event.preventDefault();
        }
    });
});
kenkenDirectives.directive('ngDownArrow', () => (scope, element, attrs) => {
    var downArrowKeyCode: number = 40;

    element.bind("keydown keypress", event=> {
        if (event.which === downArrowKeyCode) {
            scope.$apply(() => {
                scope.$eval(attrs.ngDownArrow);
            });

            event.preventDefault();
        }
    });
});
kenkenDirectives.directive('ngUpArrow', () => (scope, element, attrs) => {

    var upArrowKeyCode: number = 38;

    element.bind("keydown keypress", event=> {
        if (event.which === upArrowKeyCode) {
            scope.$apply(() => {
                scope.$eval(attrs.ngUpArrow);
            });

            event.preventDefault();
        }
    });
});
kenkenDirectives.directive('ngLeftArrow', () => (scope, element, attrs) => {
    var leftArrowKeyCode: number = 37;

    element.bind("keydown keypress", event=> {
        if (event.which === leftArrowKeyCode) {
            scope.$apply(() => {
                scope.$eval(attrs.ngLeftArrow);
            });

            event.preventDefault();
        }
    });
});
kenkenDirectives.directive('ngRightArrow', () => (scope, element, attrs) => {
    var rightArrowKeyCode: number = 39;

    element.bind("keydown keypress", event=> {
        if (event.which === rightArrowKeyCode) {
            scope.$apply(() => {
                scope.$eval(attrs.ngRightArrow);
            });

            event.preventDefault();
        }
    });
});