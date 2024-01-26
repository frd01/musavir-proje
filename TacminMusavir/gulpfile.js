/// <binding AfterBuild='default' />
const { src, dest, task, watch, series, parallel } = require('gulp');

var less = require('gulp-less');
var autoprefixer = require('gulp-autoprefixer');
var uglify = require('gulp-uglify');
var babelify = require('babelify');
var browserify = require('browserify');
var source = require('vinyl-source-stream');
var buffer = require('vinyl-buffer');
var stripDebug = require('gulp-strip-debug');
var rename = require('gulp-rename');
var sourcemaps = require('gulp-sourcemaps');
var notify = require('gulp-notify');
var plumber = require('gulp-plumber');
var options = require('gulp-options');
var gulpif = require('gulp-if');
var browserSync = require('browser-sync').create();
var npmDist = require('gulp-npm-dist');
var rename = require('gulp-rename');

var styleSRC = './assets/tacmin/styles/style.less';
var styleURL = './assets/lib/tacmin/';
var mapURL = './';

var jsSRC = './assets/tacmin/scripts/';
var jsFront = 'init.js';
var jsFiles = [jsFront];
var jsURL = './assets/lib/tacmin/';

var styleWatch = './assets/tacmin/styles/*.less';
var jsWatch = './assets/tacmin/scripts/*.js';

function css(done) {
    src([styleSRC])
        .pipe(sourcemaps.init())
        .pipe(less({
            errLogToConsole: true,
            outputStyle: 'compressed'
        }))
        .on('error', console.error.bind(console))
        .pipe(autoprefixer())
        .pipe(rename({ suffix: '.min' }))
        .pipe(sourcemaps.write(mapURL))
        .pipe(dest(styleURL))
        .pipe(browserSync.stream());
    done();
};

function js(done) {
    jsFiles.map(function(entry) {
        return browserify({
                entries: [jsSRC + entry],
                standalone: '$.ty'
            })
            .transform(babelify, { presets: ['@babel/preset-env'] })
            .bundle()
            .pipe(source(entry))
            .pipe(rename({
                extname: '.min.js'
            }))
            .pipe(buffer())
            .pipe(gulpif(options.has('production'), stripDebug()))
            .pipe(sourcemaps.init({ loadMaps: true }))
            .pipe(uglify())
            .pipe(sourcemaps.write('.'))
            .pipe(dest(jsURL))
            .pipe(browserSync.stream());
    });
    done();
};

function triggerPlumber(src_file, dest_file) {
    return src(src_file)
        .pipe(plumber())
        .pipe(dest(dest_file));
}

function watch_files() {
    watch(styleWatch, css);
    watch(jsWatch, js);
    src(jsURL + 'init.min.js')
        .pipe(notify({ message: 'Gulp is Watching!' }));
}

const nodeModules = './node_modules';
const targetPath = './assets/lib/node_modules';

function copylibs(cb) {
    src(npmDist(), { base: nodeModules })
        .pipe(rename(function(path) {
            path.dirname = path.dirname.replace(/\/dist/, '').replace(/\\dist/, '');
        }))
        .pipe(dest(targetPath))
    cb();
};

task('css', css);
task('js', js);
task('default', parallel(css, js));
task('watch', watch_files);
task('copylibs', copylibs);