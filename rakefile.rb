require 'rubygems'

VISUAL_STUDIO_PATH = ENV['VISUAL_STUDIO_PATH'] || 'C:\\Program Files\\Microsoft Visual Studio 9.0\\Common7\\IDE'
require 'iron_hammer/tasks'

class ProjectVersion 
	attr_accessor :major
	attr_accessor :minor
	attr_accessor :revision
	attr_accessor :build
	
	def self.parse v
		splitted = v.split '.'
		version = ProjectVersion.new
		version.major = splitted[0].to_i
		version.minor = splitted[1].to_i
		version.revision = splitted[2].to_i
		version.build = splitted[3].to_i
		version
	end
	
	def to_s
		"#{@major}.#{@minor}.#{@revision}.#{@build}"
	end
end

namespace :iron do
	namespace :version do
		desc 'back to 1.0.0.*'
		task :reset do
			@anvil.projects.each do |project|
				old_version = ProjectVersion.parse project.version
				old_version.major = 1
				old_version.minor = 0
				old_version.revision = 0
				project.version = v.to_s
			end
		end
		
		namespace :bump do
			desc 'bumps the major version'
			task :major do
				@anvil.projects.each do |project|
				  old_version = ProjectVersion.parse project.version
				  old_version.major += 1
				  old_version.minor = 0
				  old_version.revision = 0
				  project.version = old_version.to_s
				end
			end
			
			desc 'bumps the minor version'
			task :minor do
				@anvil.projects.each do |project|
				  old_version = ProjectVersion.parse project.version
				  old_version.minor += 1
				  old_version.revision = 0
				  project.version = old_version.to_s
				end
			end
			
			desc 'bumps the revision version'
			task :revision do
				@anvil.projects.each do |project|
				  old_version = ProjectVersion.parse project.version
				  old_version.revision += 1
				  project.version = old_version.to_s
				end
			end
		end
	end
end