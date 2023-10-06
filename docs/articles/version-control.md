Version Control
==========

A repository is represented in Bonsai by the [`CreateRepository`](xref:AllenNeuralDynamics.VersionControl.CreateRepository) operator. Several properties are exposed via this object, including the name and hash of the current commit.

In order to make it easier for users to evaluate the state of the local repository, an additional operator, [`IsRepositoryClean`](xref:AllenNeuralDynamics.VersionControl.IsRepositoryClean), is provided to check if the repository is in a clean state (i.e. are there any untracked or uncommitted changes).

:::workflow
![PredictPoseIdentities](~/workflows/CheckRepositoryStatus.bonsai)
:::