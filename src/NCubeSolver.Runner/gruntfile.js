module.exports = function (grunt) {
    grunt.initConfig({
        copy: {
            plugins: {
                files: [
                    {
                        expand: true,
                        cwd: '.',
						flatten: true,
                        src: ['../Plugins/Plugin.*/bin/Debug/*', '!../Plugins/Plugin.*/bin/Debug/*Test*'],
                        dest: 'bin/Debug/Extensions/'
                    }
                ]
            }
        },
        watch: {
            gruntFile: {
                files: ['gruntfile.js'],
                options: {
                    reload: true
                }
            },
            plugins: {
                files: ['../Plugins/Plugin.*/bin/Debug/*', '!../Plugins/Plugin.*/bin/Debug/*Test*'],
                tasks: ['newer:copy:plugins'],
                options: {
                    spawn: false,
					interrupt: true,
					atBegin: true
                }
            }
        }
    });

    // Load the plugins
    
    grunt.loadNpmTasks('grunt-newer');
	grunt.loadNpmTasks('grunt-contrib-watch');
	grunt.loadNpmTasks('grunt-contrib-copy');


    // Default task(s).
    grunt.registerTask('default', ['watch']);
};