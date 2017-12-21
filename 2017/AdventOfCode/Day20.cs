using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using AdventOfCode.Supporting;

namespace AdventOfCode
{
    public class Day20
    {

        public static int SolvePart1(string[] input)
        {
            var particles = ParseParticles(input);
            for (var i = 0; i < 1000; i++)
            {
                particles.ForEach(x => x.MoveParticle());
            }

            var smallestDistance = particles.Min(x => x.DistanceFromZero());
            return particles.IndexOf(particles.First(x => x.DistanceFromZero() == smallestDistance));
        }

        public static int SolvePart2(string[] input)
        {
            var particles = ParseParticles(input);
            for (var i = 0; i < 100; i++)
            {
                particles.ForEach(x => x.MoveParticle());
                particles.RemoveAll(x =>
                    particles.GroupBy(y => y.Position).Where(y => y.Count() > 1).Select(y => y.Key).Contains(x.Position));
            }

            return particles.Count;
        }

        public static List<Particle> ParseParticles(string[] input)
        {
            var regex = new Regex(@"p=<([-0-9]+),([-0-9]+),([-0-9]+)>, v=<([-0-9]+),([-0-9]+),([-0-9]+)>, a=<([-0-9]+),([-0-9]+),([-0-9]+)>");
            var particles = new List<Particle>();
            foreach (var g in input.Select(x => regex.Match(x).Groups))
            {
                particles.Add(new Particle(0,
                    new Vector3(int.Parse(g[1].ToString()), int.Parse(g[2].ToString()), int.Parse(g[3].ToString())),
                    new Vector3(int.Parse(g[4].ToString()), int.Parse(g[5].ToString()), int.Parse(g[6].ToString())),
                    new Vector3(int.Parse(g[7].ToString()), int.Parse(g[8].ToString()), int.Parse(g[9].ToString()))
                ));
            }

            return particles;
        }

        public class Particle
        {
            public int Id { get; }
            public Vector3 Position { get; private set; }
            public Vector3 Velocity { get; private set; }
            public Vector3 Acceleration { get; }

            public Particle(int id, Vector3 position, Vector3 velocity, Vector3 acceleration) : this(position, velocity, acceleration)
            {
                Id = id;
            }

            public Particle(Vector3 position, Vector3 velocity, Vector3 acceleration)
            {
                Position = position;
                Velocity = velocity;
                Acceleration = acceleration;
            }

            public void MoveParticle()
            {
                Velocity = Velocity + Acceleration;
                Position = Position + Velocity;
            }

            public int DistanceFromZero()
            {
                return (int)(Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z));
            }
        }
    }
}
